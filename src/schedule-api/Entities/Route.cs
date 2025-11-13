using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schedule_api.Entities
{
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteId { get; set; }

        public required int TopLevelRouteId { get; set; }

        public TopLevelRoute? TopLevelRoute { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public TimeSpan Frequency { get; set; }

        public ScheduleDay ScheduleDay { get; set; }

        public List<Schedule> RouteSchedule { get; set; } = new();
    }
}
