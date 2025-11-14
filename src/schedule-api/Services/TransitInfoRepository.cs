using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;
using schedule_api.Models;
using schedule_api.Utilities;

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

            try
            {
                return await context.Routes
                    .Where(r => r.TopLevelRouteId == topLevelRouteId && r.ScheduleDay == scheduleDay)
                    .Select(r => r.RouteId)
                    .SingleAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Invalid topLevelRouteId: {topLevelRouteId}/{dayOfWeek}");
            }
        }

        public async Task<RouteInfoModel> GetStops(int routeId)
        {
            // TODO: convert schedule offset to int offset in minutes
            // SQLLite doesn't support sorting by TimeSpan directly and LINQ
            // And OrderBy is deferred until GetEnumerator is called, resulting in this kludginess.
            var query = from stop in context.Stops
                        join routeSchedule in context.RouteSchedules
                            on stop.StopId equals routeSchedule.StopId
                        where routeSchedule.RouteId == routeId
                        select new { stop.StopId, stop.Address, routeSchedule.ScheduleOffset };

            var result = await query.ToListAsync();

            List<StopModel> stops = new();
            foreach (var rs in result.OrderBy(rs => rs.ScheduleOffset))
            {
                stops.Add(new StopModel(rs.StopId, rs.Address));
            }

            return new RouteInfoModel()
            {
                RouteId = routeId,
                Stops = stops,
            };
        }

        public async Task<Entities.Route> GetRouteById(int routeId)
        {
            try
            {
                return await context.Routes
                    .Where(r => r.RouteId == routeId)
                    .SingleAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Invalid routeId: {routeId}");
            }
        }

        public async Task<Stop> GetStopById(int stopId)
        {
            try
            {
                return await context.Stops
                    .Where(s => s.StopId == stopId)
                    .SingleAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Invalid stopId: {stopId}");
            }
        }

        public async Task<Schedule> GetScheduleByRouteAndStopId(int routeId, int stopId)
        {
            try
            {
                return await context.RouteSchedules
                    .Where(rs => rs.RouteId == routeId && rs.StopId == stopId)
                    .SingleAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Invalid routeId ({routeId})/stopId ({stopId}) combination.");
            }
        }
    }
}
