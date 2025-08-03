using System.Security.Claims;
using KidsLearning.Backend.Data;
using KidsLearning.Backend.DTOs;
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

        var children = await _context.Children
            .Where(c => c.ParentId == parentId)
            .Include(c => c.Progress)
            .Select(c => new ChildDto
            {
                ChildId = c.Id,
                Name = c.Name,
                AvatarUrl = c.AvatarUrl,
                LastActivity = "Heute",
                Progress = c.Progress.Select(p => new SubjectProgressDto
                {
                    SubjetName = p.SubjectName,
                    ProgressPercentage = p.ProgressPercentage
                }).ToList()
            }).ToListAsync();

        var dashboardData = new ParentDashboardDto
        {
            WelcomeMessage = $"Welcome to the Parent Dashboard, {parent?.UserName ?? "dear User"}!",
            Children = children,
            RecentActivities = new List<string> { "Kind 1 hat Mathe abgeschlossen", "Kind 2 hat ein neues Profilbild hochgeladen" }
        };
       
        return Ok(dashboardData);
    }
}