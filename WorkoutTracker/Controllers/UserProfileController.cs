using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Entities.Models;

namespace WorkoutTracker.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public UserProfileController(WorkoutContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAll()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserProfile>> Create(UserProfile user)
        {
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null) return NotFound();

            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
