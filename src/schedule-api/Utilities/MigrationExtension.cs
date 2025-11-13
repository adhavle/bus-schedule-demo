using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;

namespace schedule_api.Utilities
{
    public static class MigrationExtension
    {
        public static void ApplyPendingMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TransitInfoContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    logger.LogInformation("Create DB (apply pending migrations)");
                    dbContext.Database.Migrate();
                }
                else
                {
                    logger.LogInformation("No pending migrations found.");
                }
            }
        }
    }
}
