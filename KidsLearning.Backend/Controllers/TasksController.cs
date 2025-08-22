using KidsLearning.Backend.Data;
using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsLearning.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("by-subject/{subject}")] // Route eindeutig machen
    public async Task<ActionResult<List<LearningTask>>> GetTasksBySubject(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            return BadRequest("Fachname muss angegeben werden.");
        }

        var tasks = await _context.LearningTasks
            .Where(t => t.Subject == subject)
            .ToListAsync();

        if (tasks == null || !tasks.Any())
        {
            return NotFound($"Keine Aufgaben für das Fach '{subject}' gefunden.");
        }

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LearningTask>> GetLearningTask(int id)
    {
        var learningTask = await _context.LearningTasks
            .Include(lt => lt.Questions)
            .FirstOrDefaultAsync(lt => lt.Id == id);

        if (learningTask == null)
        {
            return NotFound();
        }

        return Ok(learningTask);
    }
}