using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Entities.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int WorkoutTypeId { get; set; }
        public virtual WorkoutType WorkoutType { get; set; } = null!;

        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public string? Notes { get; set; }
    }
}
