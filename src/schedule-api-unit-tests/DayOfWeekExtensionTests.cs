using schedule_api.Entities;
using schedule_api.Utilities;

namespace schedule_api_unit_tests
{
    [TestClass]
    public class DayOfWeekExtensionTests
    {
        [TestMethod]
        public void ValidateGetScheduleDayExtensionMethod()
        {
            Assert.AreEqual(ScheduleDay.Weekdays, DayOfWeek.Monday.GetScheduleDay());
            Assert.AreEqual(ScheduleDay.Weekdays, DayOfWeek.Friday.GetScheduleDay());
            Assert.AreEqual(ScheduleDay.Saturday, DayOfWeek.Saturday.GetScheduleDay());
            Assert.AreEqual(ScheduleDay.Sunday, DayOfWeek.Sunday.GetScheduleDay());
        }
    }
}
