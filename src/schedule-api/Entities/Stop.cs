using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schedule_api.Entities
{
    public class Stop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StopId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public List<Schedule> RouteSchedule { get; set; } = new();

        public Stop(int stopId, string address)
        {
            StopId = stopId;
            Address = address;
        }
    }
}
