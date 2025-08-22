using System.Security.Claims;
using KidsLearning.Backend.Data;
using KidsLearning.Backend.DTOs;
using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsLearning.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ParentDashboardController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;

    public ParentDashboardController(UserManager<IdentityUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardData()
    {
        var parentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (parentId == null)
        {
            return Unauthorized(new { Message = "User not authenticated." });
        }

        var parent = await _userManager.FindByIdAsync(parentId);

        var children = await _context.Children
            .Where(c => c.ParentId == parentId)
            .ToListAsync();

        var allSubjects = await _context.LearningTasks.Select(t => t.Subject).Distinct().ToListAsync();

        var childrenDtos = new List<ChildDto>();
        var recentActivities = new List<string>();

        foreach (var child in children)
        {
            var childProgressList = new List<SubjectProgressDto>();

            foreach (var subject in allSubjects)
            {
                var totalTasks = await _context.LearningTasks.CountAsync(t => t.Subject == subject);
                var completedTasks = await _context.ChildCompletedTasks
                    .CountAsync(ct => ct.ChildId == child.Id &&
                                    _context.LearningTasks.Any(lt => lt.Id == ct.LearningTaskId && lt.Subject == subject));

                int progressPercentage = (totalTasks > 0) ? (int)Math.Round((double)completedTasks * 100 / totalTasks) : 0;

                childProgressList.Add(new SubjectProgressDto
                {
                    SubjectName = subject,
                    ProgressPercentage = progressPercentage
                });
            }

            var latestCompletion = await _context.ChildCompletedTasks
                .Include(ct => ct.LearningTask)
                .Where(ct => ct.ChildId == child.Id)
                .OrderByDescending(ct => ct.CompletedAt)
                .FirstOrDefaultAsync();

            string lastActivityMessage = "Keine Aktivitäten";
            if (latestCompletion != null)
            {
                lastActivityMessage = latestCompletion.CompletedAt.ToString("dd.MM.yyyy HH:mm");
                recentActivities.Add($"{child.Name} hat '{latestCompletion?.LearningTask?.Title}' in {latestCompletion?.LearningTask?.Subject} abgeschlossen am {lastActivityMessage}.");
            }
            else
            {
                recentActivities.Add($"{child.Name} hat noch nicht mit dem Lernen begonnen.");
            }

            // Avatare basierend auf Sternenanzahl abrufen
            var unlockedAvatars = await _context.Avatars
                .Where(a => child.StarCount >= a.UnlockStarRequirement)
                .ToListAsync();

            childrenDtos.Add(new ChildDto
            {
                ChildId = child.Id,
                Name = child.Name,
                AvatarUrl = child.AvatarUrl,
                DateOfBirth = child.DateOfBirth,
                Difficulty = child.Difficulty,
                Age = (DateTime.Now - child.DateOfBirth).TotalDays > 0 ? (int)((DateTime.Now - child.DateOfBirth).TotalDays / 365.25) : 0,
                LastActivity = lastActivityMessage,
                Progress = childProgressList,
                StarCount = child.StarCount,
                // Die Liste der freigeschalteten Avatare in DTOs umwandeln
                UnlockedAvatars = unlockedAvatars.Select(a => new AvatarDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    Description = a.Description,
                    UnlockStarRequirement = a.UnlockStarRequirement
                }).ToList(),
                Badges = child.Badges
            });
        }

        recentActivities = recentActivities.OrderByDescending(a => a).ToList();

        var dashboardData = new ParentDashboardDto
        {
            WelcomeMessage = $"Willkommen im Eltern-Dashboard, {parent?.UserName ?? "lieber Benutzer"}!",
            Children = childrenDtos,
            RecentActivities = recentActivities
        };

        return Ok(dashboardData);
    }

    [HttpPost("add-child")]
    public async Task<IActionResult> AddChild([FromBody] AddChildDto addChildDto)
    {
        var parentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (parentId == null)
        {
            return Unauthorized(new { Message = "Parent ID claim missing." });
        }

        Console.WriteLine($"AddChild: ParentId = {parentId}, ChildName = {addChildDto.Name}");

        var newChild = new Child
        {
            Name = addChildDto.Name,
            AvatarUrl = addChildDto.AvatarUrl ?? "https://via.placeholder.com/40",
            ParentId = parentId,
            DateOfBirth = addChildDto.DateOfBirth,
            Difficulty = addChildDto.Difficulty,
        };

        _context.Children.Add(newChild);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Neues Kind gespeichert: Id={newChild.Id}, Name={newChild.Name}, ParentId={newChild.ParentId}");

        return Ok(new { Message = $"Kind '{newChild.Name}' wurde erfolgreich hinzugefügt." });
    }

    [HttpDelete("remove-child/{childId}")]
    public async Task<IActionResult> RemoveChild(int childId)
    {
        var parentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (parentId == null)
        {
            return Unauthorized(new { Message = "Parent ID claim missing." });
        }

        var child = await _context.Children.FirstOrDefaultAsync(c => c.Id == childId && c.ParentId == parentId);
        if (child == null)
        {
            return NotFound(new { Message = "Kind nicht gefunden oder nicht zugeordnet." });
        }

        _context.Children.Remove(child);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Kind '{child.Name}' wurde erfolgreich entfernt." });
    }

    [HttpPut("edit-child/{childId}")]
    public async Task<IActionResult> EditChild(int childId, [FromBody] EditChildDto editChildDto)
    {
        var parentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (parentId == null)
        {
            return Unauthorized(new { Message = "Parent ID claim missing." });
        }

        var child = await _context.Children.FirstOrDefaultAsync(c => c.Id == childId && c.ParentId == parentId);
        if (child == null)
        {
            return NotFound(new { Message = "Kind nicht gefunden oder nicht zugeordnet." });
        }

        child.Name = editChildDto.Name;
        child.DateOfBirth = editChildDto.DateOfBirth;
        child.AvatarUrl = editChildDto.AvatarUrl ?? "https://via.placeholder.com/40";
        child.Difficulty = editChildDto.Difficulty;

        _context.Children.Update(child);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Kind '{child.Name}' wurde erfolgreich aktualisiert." });
    }
}