using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Entities.Models;

namespace WorkoutTracker.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutSessionController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public WorkoutSessionController(WorkoutContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutSession>>> GetAll()
        {
            return await _context.WorkoutSessions
                .Include(ws => ws.WorkoutType)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutSession>> Get(int id)
        {
            var session = await _context.WorkoutSessions
                .Include(ws => ws.WorkoutType)
                .FirstOrDefaultAsync(ws => ws.Id == id);

            if (session == null) return NotFound();
            return session;
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutSession>> Create(WorkoutSession session)
        {
            _context.WorkoutSessions.Add(session);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = session.Id }, session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutSession session)
        {
            if (id != session.Id) return BadRequest();

            _context.Entry(session).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.WorkoutSessions.FindAsync(id);
            if (session == null) return NotFound();

            _context.WorkoutSessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
