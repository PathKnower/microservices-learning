using MicroservicesLearning.CommandsService.Models;
using MicroservicesLearning.CommandsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.ReturnAllPlatforms();
                SeedData(serviceScope.ServiceProvider.GetService<IPlatformRepo>(), platforms);
            }
        }

        private static void SeedData(IPlatformRepo platformRepo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms...");

            foreach (var platform in platforms)
            {
                if (!platformRepo.ExternalPlatformExists(platform.ExternalId))
                {
                    platformRepo.CreatePlatform(platform);
                }
            }
            platformRepo.SaveChanges();
        }
    }
}
