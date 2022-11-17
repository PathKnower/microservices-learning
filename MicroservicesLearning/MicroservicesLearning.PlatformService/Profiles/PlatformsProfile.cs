using AutoMapper;
using MicroservicesLearning.PlatformService.Dtos;
using MicroservicesLearning.PlatformService.Models;

namespace MicroservicesLearning.PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}
