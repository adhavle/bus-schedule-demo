using schedule_api.Entities;
using schedule_api.Models;

namespace schedule_api.Services
{
    public interface ITransitInfoRepository
    {
        Task<IEnumerable<TopLevelRouteInfoModel>> GetRoutes();

        Task<int> GetRouteIdForDay(int topLevelRouteId, DayOfWeek dayOfWeek);

        Task<RouteInfoModel> GetStops(int routeId);

        Task<Entities.Route> GetRouteById(int routeId);

        Task<Stop> GetStopById(int stopId);

        Task<Schedule> GetScheduleByRouteAndStopId(int routeId, int stopId);
    }
}
