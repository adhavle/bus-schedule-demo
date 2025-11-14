namespace schedule_api.Models
{
    public class NextScheduledTimeModel
    {
        public int StopId { get; set; }

        public string Address { get; set; }

        public DateTime? NextScheduledTime { get; set; }

        public bool IsServiceEndedForDay { get; set; }

        public NextScheduledTimeModel(int stopId, string address, DateTime? next, bool isEndOfService)
        {
            StopId = stopId;
            Address = address;
            NextScheduledTime = next;
            this.IsServiceEndedForDay = isEndOfService;
        }
    }
}
