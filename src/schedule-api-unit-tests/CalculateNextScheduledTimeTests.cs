using schedule_api.Entities;
using schedule_api.Models;

namespace schedule_api_unit_tests
{
    [TestClass]
    public class CalculateNextScheduledTimeTests
    {
        private int stopId;
        private Route route;
        private Stop stop;
        private Schedule schedule;

        public CalculateNextScheduledTimeTests()
        {
            stopId = 1;

            stop = new Stop(stopId, "Test Stop Address");

            schedule = new Schedule
            {
                RouteId = 1,
                StopId = stopId,
                ScheduleOffset = new TimeSpan(0, 5, 0), // 5 minutes after route start
            };

            route = new Route
            {
                RouteId = 1,
                TopLevelRouteId = 1,
                Start = new TimeSpan(6, 0, 0),      // 6:00 AM
                End = new TimeSpan(21, 0, 0),       // 9:00 PM
                Frequency = new TimeSpan(0, 10, 0),  // every 10 minutes
                ScheduleDay = ScheduleDay.Weekdays,
            };
        }

        [TestMethod]
        public void TestNextScheduledTime_RouteStart()
        {
            var beforeRouteStart = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                5,
                0,
                0);

            var result = NextScheduledTimeModel
                .CalculateNextScheduledTime(stopId, route, stop, schedule, beforeRouteStart);

            Assert.IsTrue(result.NextScheduledTime.HasValue);
            Assert.AreEqual(result.NextScheduledTime.Value.Hour, route.Start.Hours);
            Assert.AreEqual(result.NextScheduledTime.Value.Minute, route.Start.Minutes + schedule.ScheduleOffset.Minutes);
        }

        [TestMethod]
        public void TestNextScheduledTime_ServiceConcluded()
        {
            var afterRouteEnds = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                23,
                00,
                0);

            var result = NextScheduledTimeModel
                .CalculateNextScheduledTime(stopId, route, stop, schedule, afterRouteEnds);

            Assert.IsFalse(result.NextScheduledTime.HasValue);
            Assert.IsTrue(result.IsServiceEndedForDay);
        }

        [TestMethod]
        public void TestNextScheduledTime_Calculated()
        {
            var beforeRouteStart = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                10,
                1,
                0);

            var expectedNextScheduledTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                10,
                15,
                0);

            var result = NextScheduledTimeModel
                .CalculateNextScheduledTime(stopId, route, stop, schedule, beforeRouteStart);

            Assert.IsTrue(result.NextScheduledTime.HasValue);
            Assert.AreEqual(result.NextScheduledTime.Value, expectedNextScheduledTime);
        }
    }
}
