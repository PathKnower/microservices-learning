using AutoMapper;
using MicroservicesLearning.CommandsService.Data;
using MicroservicesLearning.CommandsService.Dtos;
using MicroservicesLearning.CommandsService.Models;
using System.Text.Json;

namespace MicroservicesLearning.CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _scopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default: 
                    break;
            }
        }

        private EventType DetermineEvent(string notificationEvent)
        {
            Console.WriteLine("--> Determining event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationEvent);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform publish event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undefined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var commandRepo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformRepo = scope.ServiceProvider.GetRequiredService<IPlatformRepo>();

                var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishDto);
                    if (!platformRepo.ExternalPlatformExists(platform.ExternalId))
                    {
                        platformRepo.CreatePlatform(platform);
                        platformRepo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exists...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undefined
    }
}
