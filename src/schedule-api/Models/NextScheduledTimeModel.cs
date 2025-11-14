using schedule_api.Entities;

namespace schedule_api.Models
{
    public class NextScheduledTimeModel
    {
        public int StopId { get; set; }

        public string Address { get; set; }

        public DateTime? NextScheduledTime { get; set; }

        public bool IsServiceEndedForDay { get; set; }

        public NextScheduledTimeModel(int stopId, string address, DateTime? next, bool isEndOfService = false)
        {
            StopId = stopId;
            Address = address;
            NextScheduledTime = next;
            this.IsServiceEndedForDay = isEndOfService;
        }

        public static NextScheduledTimeModel CalculateNextScheduledTime(
            int stopId,
            Entities.Route route,
            Stop stop,
            Schedule schedule,
            DateTime queryTime)
        {
            // Time calculations are relative to local time, and start of day.
            TimeSpan requestTime = new TimeSpan(queryTime.Hour, queryTime.Minute, queryTime.Second);

            // if service hasn't started for the day, return the first scheduled time
            if (requestTime <= route.Start)
            {
                var next = route.Start + schedule.ScheduleOffset;
                var firstStopTime = new DateTime(
                    queryTime.Year,
                    queryTime.Month,
                    queryTime.Day,
                    next.Hours,
                    next.Minutes,
                    next.Seconds);

                return new NextScheduledTimeModel(stopId, stop.Address, firstStopTime);
            }

            // Has service ended for the day?
            if (requestTime > (route.End + schedule.ScheduleOffset))
            {
                return new NextScheduledTimeModel(stopId, stop.Address, null, isEndOfService: true);
            }

            // Calculate and return the next stop time
            var minutesDelta = (int)requestTime.TotalMinutes - (int)route.Start.TotalMinutes;
            var nextBusNumber = (minutesDelta / (int)route.Frequency.TotalMinutes) + 1;

            var nextScheduledTime = new DateTime(
                queryTime.Year,
                queryTime.Month,
                queryTime.Day,
                route.Start.Hours,
                route.Start.Minutes,
                route.Start.Seconds);

            nextScheduledTime = nextScheduledTime.AddMinutes(
                route.Frequency.TotalMinutes * nextBusNumber);

            nextScheduledTime = nextScheduledTime.AddMinutes(
                (int)schedule.ScheduleOffset.TotalMinutes);

            return new NextScheduledTimeModel(stopId, stop.Address, nextScheduledTime);
        }
    }
}
