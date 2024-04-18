using Microsoft.EntityFrameworkCore;
using VendistaProject.Infrastructure;

namespace VendistaProject.Server.Core
{
    public class InitializationDataBase
    {
        private const string SeedDb = "SEED_DB";
        private const string ClearDb = "CLEAR_DB";
        public const string ConnectionString = "DefaultConnection";
        public static void Init(IHost host)
        {
            Console.WriteLine("Init database");

            var config = host.Services.GetRequiredService<IConfiguration>();

            if (config[SeedDb] != "true" && config[ClearDb] != "true")
            {
                return;
            }

            using var scope = host.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<VendistaProejctDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<InitializationDataBase>>();

            logger.LogInformation("Database rebuild started.");

            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            if (config[SeedDb] != "true")
            {
                dbContext.SaveChanges();

                logger.LogInformation("Database rebuild finished.");

                return;
            }

            logger.LogInformation("Started initialization of primary data in the database.");

            dbContext.SaveChanges();

            logger.LogInformation("Database rebuild finished.");
        }
    }
}
