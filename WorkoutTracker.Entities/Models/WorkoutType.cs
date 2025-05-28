using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Entities.Models
{
    public class WorkoutType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();
    }
}
