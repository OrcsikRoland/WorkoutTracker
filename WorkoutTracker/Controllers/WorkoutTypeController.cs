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
    public class WorkoutTypeController : ControllerBase
    {
        private readonly WorkoutTypeService _service;

        public WorkoutTypeController(WorkoutTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutTypeDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkoutTypeDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutTypeDTO>> Create(WorkoutTypeDTO dto)
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
    }
}
