using MicroservicesLearning.CommandsService.Models;

namespace MicroservicesLearning.CommandsService.SyncDataServices.Grpc
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}
