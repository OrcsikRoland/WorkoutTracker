using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Entities.Models;
using WorkoutTracker.Logic.DTOs;

namespace WorkoutTracker.Logic.Services
{
    public class WorkoutTypeService
    {
        private readonly WorkoutContext _context;

        public WorkoutTypeService(WorkoutContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutTypeDTO>> GetAllAsync()
        {
            var types = await Task.FromResult(_context.WorkoutTypes.Select(t => new WorkoutTypeDTO
            {
                Id = t.Id,
                Name = t.Name
            }));

            return types;
        }
        public async Task<WorkoutTypeDTO> UpdateAsync(int id, WorkoutTypeDTO dto)
        {
            var type = await _context.WorkoutTypes.FindAsync(id);
            if (type == null)
            {
                throw new KeyNotFoundException("Workout type not found.");
            }

            type.Name = dto.Name;
            await _context.SaveChangesAsync();

            return new WorkoutTypeDTO
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        public async Task<WorkoutTypeDTO?> GetByIdAsync(int id)
        {
            var type = _context.WorkoutTypes.FirstOrDefault(t => t.Id == id);
            if (type == null) return null;

            return await Task.FromResult(new WorkoutTypeDTO
            {
                Id = type.Id,
                Name = type.Name
            });
        }

        public async Task<WorkoutTypeDTO> CreateAsync(WorkoutTypeDTO dto)
        {
            var type = new WorkoutType { Name = dto.Name };
            _context.WorkoutTypes.Add(type);
            await _context.SaveChangesAsync();

            dto.Id = type.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var type = _context.WorkoutTypes.Find(id);
            if (type == null) throw new KeyNotFoundException("Workout type not found");

            _context.WorkoutTypes.Remove(type);
            await _context.SaveChangesAsync();
        }
    }
}
