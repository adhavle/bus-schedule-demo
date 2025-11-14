namespace schedule_api.Models
{
    public class StopModel
    {
        public int StopId { get; set; }

        public string Address { get; set; }

        public StopModel(int stopId, string address)
        {
            StopId = stopId;
            Address = address;
        }
    }
}
