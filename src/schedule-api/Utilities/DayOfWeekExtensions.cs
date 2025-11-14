using schedule_api.Entities;

namespace schedule_api.Utilities
{
    public static class DayOfWeekExtensions
    {
        public static ScheduleDay GetScheduleDay(this DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Saturday:
                    return ScheduleDay.Saturday;
                case DayOfWeek.Sunday:
                    return ScheduleDay.Sunday;
                default:
                    return ScheduleDay.Weekdays;
            }
        }
    }
}
