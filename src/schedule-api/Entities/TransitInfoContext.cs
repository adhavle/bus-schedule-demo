using Microsoft.EntityFrameworkCore;

namespace schedule_api.Entities
{
    public class TransitInfoContext : DbContext
    {
        public DbSet<TopLevelRoute> TopLevelRoutes { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }

        /// <summary>
        /// Constructor for injecting DbContext options
        /// </summary>
        public TransitInfoContext(DbContextOptions<TransitInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TimeSpan AM_6 = TimeSpan.FromHours(6);
            TimeSpan AM_9 = TimeSpan.FromHours(9);
            TimeSpan PM_9 = TimeSpan.FromHours(23);
            TimeSpan MINS_0 = TimeSpan.FromMinutes(0);
            TimeSpan MINS_5 = TimeSpan.FromMinutes(5);
            TimeSpan MINS_10 = TimeSpan.FromMinutes(10);
            TimeSpan MINS_15 = TimeSpan.FromMinutes(15);
            TimeSpan MINS_20 = TimeSpan.FromMinutes(20);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.TopLevelRoute)
                .WithMany(tlr => tlr.ChildRoutes)
                .HasForeignKey(r => r.TopLevelRouteId);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.RouteId, s.StopId });

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Route)
                .WithMany(r => r.RouteSchedule)
                .HasForeignKey(s => s.RouteId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Stop)
                .WithMany(st => st.RouteSchedule)
                .HasForeignKey(s => s.StopId);

            modelBuilder.Entity<TopLevelRoute>().HasData(
                new TopLevelRoute() { TopLevelRouteId = 1, TopLevelRouteName = "DASH Downtown F Northbound" },
                new TopLevelRoute() { TopLevelRouteId = 2, TopLevelRouteName = "DASH Downtown F Southbound" }
            );

            modelBuilder.Entity<Route>().HasData(
                new { RouteId = 1, TopLevelRouteId = 1, Start = AM_6, End = PM_9, Frequency = MINS_10, ScheduleDay = ScheduleDay.Weekdays },
                new { RouteId = 2, TopLevelRouteId = 1, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Saturday },
                new { RouteId = 3, TopLevelRouteId = 1, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Sunday },
                new { RouteId = 4, TopLevelRouteId = 2, Start = AM_6, End = PM_9, Frequency = MINS_10, ScheduleDay = ScheduleDay.Weekdays },
                new { RouteId = 5, TopLevelRouteId = 2, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Saturday },
                new { RouteId = 6, TopLevelRouteId = 2, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Sunday }
            );

            modelBuilder.Entity<Stop>().HasData(
                new { StopId = 1, Address = "Vermont Ave & Exposition Blvd" },
                new { StopId = 2, Address = "Vermont Ave & 37th Pl" },
                new { StopId = 3, Address = "Vermont Ave & 36th Pl" },
                new { StopId = 4, Address = "Jefferson Blvd & Vermont Ave" },
                new { StopId = 5, Address = "Jefferson Blvd & McClintock Ave" }
            );

            modelBuilder.Entity<Schedule>().HasData(
                // Route 1 Stops
                new { RouteId = 1, StopId = 1, ScheduleOffset = MINS_0 },
                new { RouteId = 1, StopId = 2, ScheduleOffset = MINS_5 },
                new { RouteId = 1, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 1, StopId = 4, ScheduleOffset = MINS_15 },
                new { RouteId = 1, StopId = 5, ScheduleOffset = MINS_20 },

                // Route 2 Stops
                new { RouteId = 2, StopId = 1, ScheduleOffset = MINS_0 },
                new { RouteId = 2, StopId = 2, ScheduleOffset = MINS_5 },
                new { RouteId = 2, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 2, StopId = 4, ScheduleOffset = MINS_15 },
                new { RouteId = 2, StopId = 5, ScheduleOffset = MINS_20 },

                // Route 3 Stops
                new { RouteId = 3, StopId = 1, ScheduleOffset = MINS_0 },
                new { RouteId = 3, StopId = 2, ScheduleOffset = MINS_5 },
                new { RouteId = 3, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 3, StopId = 4, ScheduleOffset = MINS_15 },
                new { RouteId = 3, StopId = 5, ScheduleOffset = MINS_20 },

                // Route 4 Stops
                new { RouteId = 4, StopId = 5, ScheduleOffset = MINS_0 },
                new { RouteId = 4, StopId = 4, ScheduleOffset = MINS_5 },
                new { RouteId = 4, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 4, StopId = 2, ScheduleOffset = MINS_15 },
                new { RouteId = 4, StopId = 1, ScheduleOffset = MINS_20 },

                // Route 5 Stops
                new { RouteId = 5, StopId = 5, ScheduleOffset = MINS_0 },
                new { RouteId = 5, StopId = 4, ScheduleOffset = MINS_5 },
                new { RouteId = 5, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 5, StopId = 2, ScheduleOffset = MINS_15 },
                new { RouteId = 5, StopId = 1, ScheduleOffset = MINS_20 },

                // Route 6 Stops
                new { RouteId = 6, StopId = 5, ScheduleOffset = MINS_0 },
                new { RouteId = 6, StopId = 4, ScheduleOffset = MINS_5 },
                new { RouteId = 6, StopId = 3, ScheduleOffset = MINS_10 },
                new { RouteId = 6, StopId = 2, ScheduleOffset = MINS_15 },
                new { RouteId = 6, StopId = 1, ScheduleOffset = MINS_20 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
