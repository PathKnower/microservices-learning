using MicroservicesLearning.PlatformService.Models;

namespace MicroservicesLearning.PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext appDbContext)
        {
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
