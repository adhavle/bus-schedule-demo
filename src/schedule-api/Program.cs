using Microsoft.EntityFrameworkCore;
using schedule_api.Entities;
using schedule_api.Services;
using schedule_api.Utilities;
using System.Runtime.CompilerServices;

namespace schedule_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add services to container
            builder.Services.AddControllers();

            builder.Services.AddDbContext<TransitInfoContext>(options =>
                options.UseSqlite("Data Source=TransitInfo.db"));

            builder.Services.AddScoped<ITransitInfoRepository, TransitInfoRepository>();
            
            var app = builder.Build();

            // add middlewares
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.ApplyPendingMigrations();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
