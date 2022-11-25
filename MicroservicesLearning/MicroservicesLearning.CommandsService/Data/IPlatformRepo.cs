using MicroservicesLearning.CommandsService.Models;

namespace MicroservicesLearning.CommandsService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();

        void CreatePlatform(Platform platform);

        bool ExternalPlatformExists(int externalPlatformId);
    }
}
