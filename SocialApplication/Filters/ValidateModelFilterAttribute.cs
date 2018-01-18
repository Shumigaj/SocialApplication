using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialApplication.Models;

namespace SocialApplication.Filters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            context.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            var errorDetails = new ErrorDetails("Model validation was failed", context.ModelState.Values.SelectMany(v => v.Errors));
            context.Result = new BadRequestObjectResult(errorDetails);
        }
    }
}
