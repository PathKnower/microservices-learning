using MicroservicesLearning.CommandsService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroservicesLearning.CommandsService.Attributes
{
    public class CheckPlatformExistsServiceFilter : ActionFilterAttribute
    {
        private readonly ICommandRepo _commandRepo;

        public CheckPlatformExistsServiceFilter(ICommandRepo commandRepo)
        {
            _commandRepo = commandRepo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var platformId = (int)context.ActionArguments["platformId"];
            if (!_commandRepo.PlatformExists(platformId))
            {
                context.Result = new NotFoundResult();
            }
            else
                base.OnActionExecuting(context);
        }
    }
}
