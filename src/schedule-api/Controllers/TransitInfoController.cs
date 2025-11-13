using Microsoft.AspNetCore.Mvc;
using schedule_api.Entities;
using schedule_api.Services;

namespace schedule_api.Controllers
{
    [ApiController]
    public class TransitInfoController : ControllerBase
    {
        private readonly ILogger<TransitInfoController> logger;
        private readonly ITransitInfoRepository transitInfo;

        public TransitInfoController(
            ILogger<TransitInfoController> logger,
            ITransitInfoRepository transitInfo)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.transitInfo = transitInfo ?? throw new ArgumentNullException(nameof(transitInfo));
        }

        /// <summary>
        /// Fetch routes
        /// </summary>
        /// <returns>TBD</returns>
        [HttpGet("transitInfo/routes")]
        public async Task<ActionResult<List<TopLevelRoute>>> GetRoutes()
        {
            logger.Log(LogLevel.Information, nameof(GetRoutes));

            var routes = await transitInfo.GetRoutes();

            return Ok(routes.Select(r => new 
            {
                topLevelRouteId = r.TopLevelRouteId,
                routeName = r.TopLevelRouteName 
            }));
        }

        /// <summary>
        /// Fetch stops, given a route
        /// </summary>
        /// <returns>TBD</returns>
        [HttpGet("transitInfo/stops/{routeId}")]
        public async Task<IActionResult> GetStops(int routeId)
        {
            logger.Log(LogLevel.Information, nameof(GetStops));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetch next scheduled time, given a route and stop
        /// </summary>
        /// <returns>TBD</returns>
        [HttpGet("transitInfo/nextScheduledTime/{routeId}/{stopId}")]
        public async Task<IActionResult> GetNextScheduledTime(int routeId, int stopId)
        {
            logger.Log(LogLevel.Information, nameof(GetStops));

            throw new NotImplementedException();
        }
    }
}
