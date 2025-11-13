using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schedule_api.Entities
{
    public class Schedule
    {
        public int RouteId { get; set; }

        public Route? Route { get; set; }

        public int StopId { get; set; }

        public Stop? Stop { get; set; }

        public TimeSpan ScheduleOffset { get; set; }
    }
}
