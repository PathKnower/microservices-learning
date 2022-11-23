using MicroservicesLearning.PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext appDbContext, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!appDbContext.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                appDbContext.Platforms.AddRange(
                    new Platform { Name = "Dot Net", Publisher="Microsoft", Cost="Free" },
                    new Platform { Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );

                appDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Data existed");
            }
        }
    }
}
