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
        public DbSet<UserProfile> UserProfiles { get; set; }

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

            // WorkoutType → WorkoutSessions (1:N)
            modelBuilder.Entity<WorkoutType>()
                .HasMany(wt => wt.Sessions)
                .WithOne(ws => ws.WorkoutType)
                .HasForeignKey(ws => ws.WorkoutTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserProfile → WorkoutSessions (1:N)
            modelBuilder.Entity<UserProfile>()
                .HasMany(up => up.Sessions)
                .WithOne()
                .HasForeignKey("UserProfileId") // shadow property
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
