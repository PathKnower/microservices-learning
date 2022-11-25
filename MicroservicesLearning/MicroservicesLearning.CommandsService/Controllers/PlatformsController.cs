using AutoMapper;
using MicroservicesLearning.CommandsService.Data;
using MicroservicesLearning.CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesLearning.CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(
            IPlatformRepo platformRepo,
            IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting platforms from CommandsService");

            var platforms = _platformRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpPost]
        public IActionResult TestInbound()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Thats fine");
        }
    }
}
