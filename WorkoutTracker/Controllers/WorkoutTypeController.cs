using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Entities.Models;

namespace WorkoutTracker.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutTypeController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public WorkoutTypeController(WorkoutContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutType>>> GetAll()
        {
            return await _context.WorkoutTypes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutType>> Create(WorkoutType type)
        {
            _context.WorkoutTypes.Add(type);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = type.Id }, type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.WorkoutTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.WorkoutTypes.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
