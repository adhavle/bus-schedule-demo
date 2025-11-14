using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using schedule_api.Controllers;
using schedule_api.Services;

namespace schedule_api_unit_tests
{
    [TestClass]
    public class TransitInfoControllerTests
    {
        private ILogger<TransitInfoController> logger;
        private ITransitInfoRepository transitInfo;
        private TransitInfoController transitInfoController;

        public TransitInfoControllerTests()
        {
            logger = NullLogger<TransitInfoController>.Instance;

            // TODO: setup a less brittle path to connect to the database
            // Mocking it would be better, but this is fine for now
            DbContextOptions<TransitInfoContext> options =
                new DbContextOptionsBuilder<TransitInfoContext>()
                    .UseSqlite(@"Data Source=..\..\..\..\schedule-api\TransitInfo.db")
                    .Options;
            var transitInfoContext = new TransitInfoContext(options);

            transitInfo = new TransitInfoRepository(transitInfoContext);
            transitInfoController = new TransitInfoController(logger, transitInfo);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            // nothing for now
        }

        [TestMethod]
        [Description("Validate response status and format for /transitInfo/routes")]
        public async Task TestGetRoutes()
        {
            var result = await transitInfoController.GetRoutes();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as IEnumerable<schedule_api.Models.TopLevelRouteInfoModel>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.All(r => !string.IsNullOrWhiteSpace(r.RouteName)));
            Assert.IsTrue(response.All(r => r.TopLevelRouteId > 0));
        }

        [TestMethod]
        [Description("Validate response status and format for /transitInfo/stops/{topLevelRouteId}")]
        public async Task TestGetStops()
        {
            int testTopLevelRouteId = 2; // known to exist in the test data
            var result = await transitInfoController.GetStops(testTopLevelRouteId);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as schedule_api.Models.RouteInfoModel;
            Assert.IsNotNull(response);

            // known mapping
            int expectedRouteId = DateTime.Now.DayOfWeek switch
            {
                DayOfWeek.Sunday => 6,
                DayOfWeek.Saturday => 5,
                _ => 4
            };

            Assert.AreEqual(expectedRouteId, response.RouteId);
            Assert.IsTrue(response.Stops.Count > 0);
            Assert.IsTrue(response.Stops.All(s => !string.IsNullOrWhiteSpace(s.Address)));
            Assert.IsTrue(response.Stops.All(s => s.StopId > 0));
        }

        [TestMethod]
        [Description("Validate exception when invalid topLevelRouteId passed to get stops")]
        public async Task TestGetStops_InvalidTopLevelRouteId()
        {
            int tlrId = 100; // known to not exist

            var result = await transitInfoController.GetStops(tlrId);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value?.ToString()?.StartsWith("Invalid topLevelRouteId"));
        }

        [TestMethod]
        [Description("Validate next scheduled time responses")]
        public async Task TestGetNextScheduledTime()
        {
            var result = await transitInfoController.GetNextScheduledTime(4, 5); // known to exist
            var okResult = result.Result as OkObjectResult;
            
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var responseValue = okResult.Value as schedule_api.Models.NextScheduledTimeModel;
            Assert.IsNotNull(responseValue);

            if (responseValue.IsServiceEndedForDay)
            {
                Assert.IsNull(responseValue.NextScheduledTime);
            }
            else
            {
                Assert.IsNotNull(responseValue.NextScheduledTime);
                Assert.IsTrue(responseValue.NextScheduledTime > DateTime.Now);
            }
        }


        [TestMethod]
        [Description("Validate exception when invalid routeId is passed to get next schedule")]
        public async Task TestGetNextSchedule_InvalidRouteId()
        {
            int routeId = 100; // known to not exist
            int stopId = 1; // known to exist

            var result = await transitInfoController.GetNextScheduledTime(routeId, stopId);
            var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value?.ToString()?.StartsWith("Invalid routeId"));
        }

        [TestMethod]
        [Description("Validate exception when invalid stopId is passed to get next schedule")]
        public async Task TestGetNextSchedule_InvalidStopId()
        {
            int routeId = 1; // known to not exist
            int stopId = 100; // known to exist

            var result = await transitInfoController.GetNextScheduledTime(routeId, stopId);
            var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value?.ToString()?.StartsWith("Invalid stopId"));
        }
    }
}
