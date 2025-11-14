using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;

namespace schedule_api.Services
{
    public class TransitInfoContext : DbContext
    {
        public DbSet<TopLevelRoute> TopLevelRoutes { get; set; }
        public DbSet<Entities.Route> Routes { get; set; }
        public DbSet<Schedule> RouteSchedules { get; set; }
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
            TimeSpan MINS_10 = TimeSpan.FromMinutes(10);
            TimeSpan MINS_15 = TimeSpan.FromMinutes(15);

            modelBuilder.Entity<Entities.Route>()
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

            modelBuilder.Entity<Entities.Route>().HasData(
                new { RouteId = 1, TopLevelRouteId = 1, Start = AM_6, End = PM_9, Frequency = MINS_10, ScheduleDay = ScheduleDay.Weekdays },
                new { RouteId = 2, TopLevelRouteId = 1, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Saturday },
                new { RouteId = 3, TopLevelRouteId = 1, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Sunday },
                new { RouteId = 4, TopLevelRouteId = 2, Start = AM_6, End = PM_9, Frequency = MINS_10, ScheduleDay = ScheduleDay.Weekdays },
                new { RouteId = 5, TopLevelRouteId = 2, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Saturday },
                new { RouteId = 6, TopLevelRouteId = 2, Start = AM_9, End = PM_9, Frequency = MINS_15, ScheduleDay = ScheduleDay.Sunday }
            );

            var StopAddresses = new List<string>()
            {
                "Beaudry Ave & 3rd St",
                "4th St & Figueroa St",
                "Flower St & 5th St",
                "Flower St & 7th St",
                "Flower St & 8th St",
                "Flower St & 9th St",
                "Flower St & Olympic Blvd",
                "Figueroa & 12th St (Crypto.com Arena)",
                "Figueroa St & Pico Blvd",
                "Figueroa St & Venice Blvd",
                "Figueroa St & Washington Blvd",
                "Figueroa St & 23rd St",
                "Figueroa St & Adams Blvd",
                "Figueroa St & 30th St",
                "Figueroa St & Jefferson Blvd",
                "Figueroa St & McCarthy Way",
                "Exposition Blvd & Trousdale Pkwy",
                "Exposition Blvd & Watt Way",
            };

            var stops = Enumerable
                .Range(1, StopAddresses.Count)
                .Select(i => new Stop() { StopId = i, Address = StopAddresses[i - 1] });

            modelBuilder.Entity<Stop>().HasData(stops);

            // Define route stops with schedule offsets
            // All stops are spaced 3 minutes apart, to facilitate generating the schedule data easily.
            // 
            var route1Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 1, StopId = i+1, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });

            var route2Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 2, StopId = i+1, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });

            var route3Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 3, StopId = i+1, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });

            var route4Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 4, StopId = StopAddresses.Count - i, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });

            var route5Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 5, StopId = StopAddresses.Count - i, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });

            var route6Stops = Enumerable
                .Range(0, StopAddresses.Count)
                .Select(i => new Schedule() { RouteId = 6, StopId = StopAddresses.Count - i, ScheduleOffset = TimeSpan.FromMinutes(i * 3) });


            modelBuilder.Entity<Schedule>().HasData(
                route1Stops
                .Concat(route2Stops)
                .Concat(route3Stops)
                .Concat(route4Stops)
                .Concat(route5Stops)
                .Concat(route6Stops)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
