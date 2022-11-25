using AutoMapper;
using MicroservicesLearning.CommandsService.Dtos;
using MicroservicesLearning.CommandsService.Models;

namespace MicroservicesLearning.CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
        }
    }
}
