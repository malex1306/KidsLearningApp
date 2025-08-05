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
        var userEmail = parent?.Email ?? "";

        
        var childrenWithProgress = await _context.Children
            .Where(c => c.ParentId == parentId)
            .Include(c => c.Progress)
            .ToListAsync();

        
        var recentActivities = new List<string>();

       
        foreach (var child in childrenWithProgress)
        {
            if (!child.Progress.Any())
            {
                recentActivities.Add($"{child.Name} hat noch nicht mit dem Lernen begonnen.");
            }
        }
        
        
        var latestProgress = childrenWithProgress
            .SelectMany(c => c.Progress)
            .OrderByDescending(p => p.LastUpdated)
            .Take(5)
            .ToList();

        foreach (var progress in latestProgress)
        {
            var childName = childrenWithProgress.FirstOrDefault(c => c.Id == progress.ChildId)?.Name;
            if (childName != null)
            {
                recentActivities.Add($"{childName} hat {progress.SubjectName} auf {progress.ProgressPercentage}% abgeschlossen.");
            }
        }

        var childrenDtos = childrenWithProgress.Select(c => new ChildDto
        {
            ChildId = c.Id,
            Name = c.Name,
            AvatarUrl = c.AvatarUrl,
            LastActivity = c.Progress.Any()
            ? c.Progress.Max(p => p.LastUpdated).ToString("dd.MM.yyyy HH:mm")
            : "Keine Aktivitäten",
            Age = DateTime.Now.Year - c.DateOfBirth.Year,
            DateOfBirth = c.DateOfBirth,
            Progress = c.Progress.Select(p => new SubjectProgressDto
            {
                SubjectName = p.SubjectName,
                ProgressPercentage = p.ProgressPercentage
            }).ToList()
        }).ToList();

        var dashboardData = new ParentDashboardDto
        {
            WelcomeMessage = $"Willkommen im Eltern-Dashboard, {parent?.UserName ?? "lieber Benutzer"}!",
            Children = childrenDtos,
            RecentActivities = recentActivities // Die dynamisch erstellte Liste
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

        _context.Children.Update(child);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Kind '{child.Name}' wurde erfolgreich aktualisiert." });
    }
    
}