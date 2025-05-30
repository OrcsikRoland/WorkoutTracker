using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Entities.Models;
using WorkoutTracker.Logic.DTOs;
using WorkoutTracker.Logic.Services;

namespace WorkoutTracker.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutSessionController : ControllerBase
    {
        private readonly WorkoutSessionService _service;

        public WorkoutSessionController(WorkoutSessionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutSessionDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkoutSessionDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutSessionDTO>> GetById(int id)
        {
            var session = await _service.GetByIdAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }
        [HttpPost]
        public async Task<ActionResult<WorkoutSessionDTO>> Create(WorkoutSessionDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("stats")]
        public async Task<ActionResult<Dictionary<string, int>>> GetWorkoutTypeStats()
        {
            var stats = await _service.GetWorkoutTypeStatsAsync();
            return Ok(stats);
        }
    }
}
