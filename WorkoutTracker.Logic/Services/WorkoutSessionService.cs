using Microsoft.EntityFrameworkCore;
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
    public class WorkoutSessionService
    {
        private readonly WorkoutContext _context;

        public WorkoutSessionService(WorkoutContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutSessionDTO>> GetAllAsync()
        {
            var sessions = await _context.WorkoutSessions
                .Include(s => s.WorkoutType)
                .ToListAsync();

            return sessions.Select(s => new WorkoutSessionDTO
            {
                Id = s.Id,
                Date = s.Date,
                WorkoutTypeId = s.WorkoutTypeId,
                WorkoutTypeName = s.WorkoutType?.Name ?? "Unknown",
                DurationMinutes = s.DurationMinutes,
                CaloriesBurned = s.CaloriesBurned,
                Notes = s.Notes
            });
        }
        public async Task<WorkoutSessionDTO?> GetByIdAsync(int id)
        {
            var session = await _context.WorkoutSessions
                .Include(s => s.WorkoutType)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return null;
            }

            return new WorkoutSessionDTO
            {
                Id = session.Id,
                Date = session.Date,
                WorkoutTypeId = session.WorkoutTypeId,
                WorkoutTypeName = session.WorkoutType?.Name ?? "Unknown",
                DurationMinutes = session.DurationMinutes,
                CaloriesBurned = session.CaloriesBurned,
                Notes = session.Notes
            };
        }
        public async Task<WorkoutSessionDTO> UpdateAsync(int id, WorkoutSessionDTO dto)
        {
            var session = await _context.WorkoutSessions.FindAsync(id);
            if (session == null)
            {
                throw new KeyNotFoundException("Workout session not found.");
            }

            session.Date = dto.Date;
            session.WorkoutTypeId = dto.WorkoutTypeId;
            session.DurationMinutes = dto.DurationMinutes;
            session.CaloriesBurned = dto.CaloriesBurned;
            session.Notes = dto.Notes;

            await _context.SaveChangesAsync();

           
            string typeName = _context.WorkoutTypes
                                .FirstOrDefault(t => t.Id == session.WorkoutTypeId)?.Name ?? "Unknown";

            return new WorkoutSessionDTO
            {
                Id = session.Id,
                Date = session.Date,
                WorkoutTypeId = session.WorkoutTypeId,
                WorkoutTypeName = typeName,
                DurationMinutes = session.DurationMinutes,
                CaloriesBurned = session.CaloriesBurned,
                Notes = session.Notes
            };
        }

        public async Task<WorkoutSessionDTO> CreateAsync(WorkoutSessionDTO dto)
        {
            var session = new WorkoutSession
            {
                Date = dto.Date,
                WorkoutTypeId = dto.WorkoutTypeId,
                DurationMinutes = dto.DurationMinutes,
                CaloriesBurned = dto.CaloriesBurned,
                Notes = dto.Notes
            };

            _context.WorkoutSessions.Add(session);
            await _context.SaveChangesAsync();

            dto.Id = session.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.WorkoutSessions.FindAsync(id);
            if (session == null) throw new KeyNotFoundException("Workout session not found");

            _context.WorkoutSessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        
        public async Task<Dictionary<string, int>> GetWorkoutTypeStatsAsync()
        {
            var stats = await _context.WorkoutSessions
                .Include(s => s.WorkoutType)
                .GroupBy(s => s.WorkoutType!.Name)
                .Select(g => new { TypeName = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.TypeName, x => x.Count);

            return stats;
        }
    }
}
