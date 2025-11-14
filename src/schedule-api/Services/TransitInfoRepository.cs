using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;
using schedule_api.Models;
using schedule_api.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace schedule_api.Services
{
    public class TransitInfoRepository : ITransitInfoRepository
    {
        private readonly TransitInfoContext context;

        public TransitInfoRepository(TransitInfoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TopLevelRouteInfoModel>> GetRoutes()
        {
            var result = await context.TopLevelRoutes
                .OrderBy(tlr => tlr.TopLevelRouteName)
                .ToListAsync();

            return result.Select(tlr => new TopLevelRouteInfoModel(tlr.TopLevelRouteId, tlr.TopLevelRouteName));
        }

        public async Task<int> GetRouteIdForDay(int topLevelRouteId, DayOfWeek dayOfWeek)
        {
            var scheduleDay = dayOfWeek.GetScheduleDay();

            return await context.Routes
                .Where(r => r.TopLevelRouteId == topLevelRouteId && r.ScheduleDay == scheduleDay)
                .Select(r => r.RouteId)
                .FirstOrDefaultAsync();
        }

        public async Task<RouteInfoModel> GetStops(int routeId)
        {
            var query = from stop in context.Stops
                        join routeSchedule in context.RouteSchedules
                            on stop.StopId equals routeSchedule.StopId
                        where routeSchedule.RouteId == routeId
                        select new { stop.StopId, stop.Address, routeSchedule.ScheduleOffset };

            var result = await query.ToListAsync();
            var result2 = result.OrderBy(rs => rs.ScheduleOffset);

            // TBD: convert schedule offset to an integer field, representing the offset in minutes
            // SQLLite doesn't support sorting by TimeSpan directly and LINQ
            // OrderBy is deferred until GetEnumerator is called, resulting in this kludginess.

            List<StopModel> stops = new();
            foreach (var rs in result2)
            {
                stops.Add(new StopModel(rs.StopId, rs.Address));
            }

            return new RouteInfoModel()
            {
                RouteId = routeId,
                Stops = stops,
            };
        }

        public async Task<NextScheduledTimeModel> GetNextScheduledTime(int routeId, int stopId, DateTime requested)
        {
            var route = await context.Routes
                .Where(r => r.RouteId == routeId)
                .FirstOrDefaultAsync();

            if (route == null)
            {
                throw new ArgumentException($"Route with ID {routeId} not found.");
            }

            var stopSchedule = await context.RouteSchedules
                .Where(rs => rs.RouteId == routeId && rs.StopId == stopId)
                .FirstOrDefaultAsync();

            if (stopSchedule == null)
            {
                throw new ArgumentException($"Stop with ID {stopId} not found for Route ID {routeId}.");
            }

            TimeSpan requestTime = new TimeSpan(requested.Hour, requested.Minute, requested.Second);
            DateTime? nextScheduledTime = null;
            bool isEndOfService = false;

            if (requestTime <= route.Start)
            {
                var next = route.Start + stopSchedule.ScheduleOffset;
                nextScheduledTime = new DateTime(
                    requested.Year,
                    requested.Month,
                    requested.Day,
                    next.Hours,
                    next.Minutes,
                    next.Seconds);
            }
            else if (requestTime > (route.End + stopSchedule.ScheduleOffset))
            {
                isEndOfService = true;
            }
            else
            {
                var minutesDelta = (int)requestTime.TotalMinutes - (int)route.Start.TotalMinutes;
                var nextBusNumber = (minutesDelta / (int)route.Frequency.TotalMinutes) + 1;

                nextScheduledTime = new DateTime(
                    requested.Year,
                    requested.Month,
                    requested.Day,
                    route.Start.Hours,
                    route.Start.Minutes,
                    route.Start.Seconds);

                nextScheduledTime = nextScheduledTime.Value.AddMinutes(
                    route.Frequency.TotalMinutes * nextBusNumber);

                nextScheduledTime = nextScheduledTime.Value.AddMinutes(
                    (int)stopSchedule.ScheduleOffset.TotalMinutes);
            }

            var stopInfo = await context.Stops
                .Where(s => s.StopId == stopId)
                .FirstOrDefaultAsync();

            if (stopInfo == null)
            {
                throw new ArgumentException($"Stop with ID {stopId} not found.");
            }

            return new NextScheduledTimeModel(
                stopId,
                stopInfo.Address ?? "Stop address unknown",
                nextScheduledTime,
                isEndOfService);
        }
    }
}
