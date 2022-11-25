using MicroservicesLearning.PlatformService.Dtos;

namespace MicroservicesLearning.PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishDto platformPublishDto);
    }
}
