using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsLearning.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ParentDashboardController : ControllerBase
{
    [HttpGet]
    public IActionResult GetDashboardData()
    {
        var userEmail = User.Identity?.Name;
        return Ok(new{ Message = $"Welcome to the Parent Dashboard, {userEmail}!" });
    }
}