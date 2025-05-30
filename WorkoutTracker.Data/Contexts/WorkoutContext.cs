using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities.Models;

namespace WorkoutTracker.Data.Contexts
{
    public class WorkoutContext : DbContext
    {       
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }

        public WorkoutContext(DbContextOptions<WorkoutContext> ctx)
        : base(ctx)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<WorkoutType>()
                .HasMany(wt => wt.Sessions)
                .WithOne(ws => ws.WorkoutType)
                .HasForeignKey(ws => ws.WorkoutTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WorkoutType>().HasData(
                new WorkoutType { Id = 1, Name = "Cardio" },
                new WorkoutType { Id = 2, Name = "Strength" },
                new WorkoutType { Id = 3, Name = "Yoga" },
                new WorkoutType { Id = 4, Name = "HIIT" }
            );

            
          
        }
    }
}
