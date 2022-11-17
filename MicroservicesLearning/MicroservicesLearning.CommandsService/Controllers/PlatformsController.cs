using Microsoft.AspNetCore.Mvc;

namespace MicroservicesLearning.CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public IActionResult TestInbound()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Thats fine");
        }
    }
}
