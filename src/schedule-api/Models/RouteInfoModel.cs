namespace schedule_api.Models
{
    public class RouteInfoModel
    {
        public int RouteId { get; set; }

        public List<StopModel> Stops { get; set; } = new();
    }
}
