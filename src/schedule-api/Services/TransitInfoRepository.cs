using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;

namespace schedule_api.Services
{
    public class TransitInfoRepository : ITransitInfoRepository
    {
        private readonly TransitInfoContext context;

        public TransitInfoRepository(TransitInfoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TopLevelRoute>> GetRoutes()
        {
            return await context.TopLevelRoutes
                .OrderBy(tlr => tlr.TopLevelRouteName)
                .ToListAsync();
        }
    }
}
