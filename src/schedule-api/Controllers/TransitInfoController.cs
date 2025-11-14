using Microsoft.AspNetCore.Mvc;
using schedule_api.Entities;
using schedule_api.Models;
using schedule_api.Services;
using System.Text.Json;

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
        /// <returns>A list of route name/id pairs</returns>
        [HttpGet("transitInfo/routes")]
        public async Task<ActionResult<List<TopLevelRouteInfoModel>>> GetRoutes()
        {
            logger.Log(LogLevel.Information, nameof(GetRoutes));

            var routes = await transitInfo.GetRoutes();

            return Ok(routes);
        }

        /// <summary>
        /// Fetch stops, given a route
        /// </summary>
        /// <returns>TBD</returns>
        [HttpGet("transitInfo/stops/{topLevelRouteId}")]
        public async Task<ActionResult> GetStops(int topLevelRouteId)
        {
            logger.Log(LogLevel.Information, nameof(GetStops));

            var routeId = await transitInfo.GetRouteIdForDay(topLevelRouteId, DateTime.Now.DayOfWeek);
            var routeStops = await transitInfo.GetStops(routeId);

            return Ok(routeStops);
        }

        /// <summary>
        /// Fetch next scheduled time, given a route and stop
        /// </summary>
        /// <returns>TBD</returns>
        [HttpGet("transitInfo/nextScheduledTime/{routeId}/{stopId}")]
        public async Task<ActionResult<NextScheduledTimeModel>> GetNextScheduledTime(int routeId, int stopId)
        {
            logger.Log(LogLevel.Information, nameof(GetStops));

            var nextScheduledTime = await transitInfo.GetNextScheduledTime(routeId, stopId, DateTime.Now);

            return Ok(nextScheduledTime);
        }
    }
}
