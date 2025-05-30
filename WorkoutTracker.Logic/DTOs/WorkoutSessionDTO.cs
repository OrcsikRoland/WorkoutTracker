using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Logic.DTOs
{
    public class WorkoutSessionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkoutTypeId { get; set; }
        public string WorkoutTypeName { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public string? Notes { get; set; }
    }
}
