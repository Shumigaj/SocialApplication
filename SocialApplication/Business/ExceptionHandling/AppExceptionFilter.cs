using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialApplication.Business.ExceptionHandling
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = ResultBuilder
                .CreateErrorResult(HttpStatusCode.InternalServerError,
                    context.Exception.Message);
        }
    }
}
