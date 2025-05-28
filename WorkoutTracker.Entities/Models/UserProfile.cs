using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Entities.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public double WeightKg { get; set; }

        public virtual ICollection<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();
    }
}
