using KidsLearning.Backend.Data;
using KidsLearning.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidsLearning.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("avatars/{childId}")]
        public async Task<ActionResult<IEnumerable<Avatar>>> GetUnlockedAvatars(int childId)
        {
            var child = await _context.Children.FindAsync(childId);
            if (child == null)
            {
                return NotFound("Kind nicht gefunden.");
            }
            
            var allAvatars = await _context.Avatars.ToListAsync();
            var unlockedAvatars = allAvatars.Where(a => child.StarCount >= a.UnlockStarRequirement).ToList();
            
            return Ok(unlockedAvatars); // Gibt die Liste der Avatar-Modelle zurück
        }
    }
}