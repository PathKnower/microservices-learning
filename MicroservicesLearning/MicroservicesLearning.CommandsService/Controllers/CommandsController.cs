using AutoMapper;
using MicroservicesLearning.CommandsService.Attributes;
using MicroservicesLearning.CommandsService.Data;
using MicroservicesLearning.CommandsService.Dtos;
using MicroservicesLearning.CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesLearning.CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CheckPlatformExistsServiceFilter))]
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

        [HttpPost]
        public ActionResult CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);

            _commandRepo.CreateCommand(platformId, command);
            _commandRepo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), 
                new { platformId = platformId, command = commandReadDto.Id},
                commandReadDto);
        }
    }
}
