using AutoMapper;
using MicroservicesLearning.CommandsService.Attributes;
using MicroservicesLearning.CommandsService.Data;
using MicroservicesLearning.CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesLearning.CommandsService.Controllers
{
    [ServiceFilter(typeof(CheckPlatformExistsServiceFilter))]
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(
            ICommandRepo commandRepo,
            IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            var commands = _commandRepo.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public IActionResult GetCommandForPlatform (int platformId, int commandId)
        {
            var command = _commandRepo.GetCommand(platformId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }
    }
}
