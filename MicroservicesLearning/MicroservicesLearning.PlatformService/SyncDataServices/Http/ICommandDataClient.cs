using MicroservicesLearning.PlatformService.Dtos;

namespace MicroservicesLearning.PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}
