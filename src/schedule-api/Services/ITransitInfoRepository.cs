using schedule_api.Entities;

namespace schedule_api.Services
{
    public interface ITransitInfoRepository
    {
        Task<IEnumerable<TopLevelRoute>> GetRoutes();
    }
}
