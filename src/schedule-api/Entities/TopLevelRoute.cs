using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schedule_api.Entities
{
    /// <summary>
    /// A TopLevelRoute represents a route (ex: "DASH Downtown F Northbound")
    /// Which has child routes (ex: weekday routes, saturday and sunday routes)
    /// There may be a need to link a TopLevelRoute to its complementary
    /// TopLevelRoute running in the opposite direction, but that is out of scope for now.
    /// </summary>
    public class TopLevelRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopLevelRouteId { get; set; }

        [MaxLength(100)]
        public required string TopLevelRouteName { get; set; }

        public List<Route> ChildRoutes { get; set; } = [];
    }
}
