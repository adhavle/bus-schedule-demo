namespace schedule_api.Entities
{
    /// <summary>
    /// Specifies the day for which a route is run.
    /// For now, managing separate routes for weekdays, or holidays is out of scope.
    /// Similarly a route that runs the same everyday including weekends is out of scope.
    /// (that can be realized using 3 separate routes, for now)
    /// </summary>
    public enum ScheduleDay
    {
        Weekdays = 0,
        Saturday = 1,
        Sunday = 2,
    }
}
