using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialApplication.Business.ExceptionHandling;

namespace SocialApplication.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errorDetails = new ErrorDetails("Model validation was failed", context.ModelState.Values.SelectMany(v => v.Errors));
            context.Result = new BadRequestObjectResult(errorDetails);
        }
    }
}
