namespace schedule_api.Models
{
    public class TopLevelRouteInfoModel
    {
        public int TopLevelRouteId { get; set; }
        public string RouteName { get; set; }

        public TopLevelRouteInfoModel(int tlrId, string routeName)
        {
            TopLevelRouteId = tlrId;
            RouteName = routeName;
        }
    }
}
