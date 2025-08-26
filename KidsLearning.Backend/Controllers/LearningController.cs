using System.Security.Claims;
using KidsLearning.Backend.Data;
using KidsLearning.Backend.DTOs;
using KidsLearning.Backend.Models;
using KidsLearning.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsLearning.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LearningController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly RewardService _rewardService;
    private readonly UserManager<IdentityUser> _userManager;

    public LearningController(AppDbContext context, UserManager<IdentityUser> userManager,
        RewardService rewardService)
    {
        _context = context;
        _userManager = userManager;
        _rewardService = rewardService;
    }

    [HttpPost("complete-task")]
    public async Task<IActionResult> CompleteTask([FromBody] ChildTaskCompletionDto completionDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();


        var child = await _context.Children.FirstOrDefaultAsync(c => c.Id == completionDto.ChildId);

        if (child == null || child.ParentId != userId) return Unauthorized(new { Message = "Zugriff verweigert." });


        var alreadyCompleted = await _context.ChildCompletedTasks
            .AnyAsync(ct => ct.ChildId == completionDto.ChildId && ct.LearningTaskId == completionDto.TaskId);

        if (!alreadyCompleted)
        {
            var completedTask = new ChildCompletedTask
            {
                ChildId = completionDto.ChildId,
                LearningTaskId = completionDto.TaskId,
                CompletedAt = DateTime.Now
            };

            _context.ChildCompletedTasks.Add(completedTask);
            await _context.SaveChangesAsync();
            await _rewardService.RewardChildForCompletedTask(child.Id);
        }

        return Ok(new { Message = "Aufgabe erfolgreich als erledigt markiert." });
    }
}