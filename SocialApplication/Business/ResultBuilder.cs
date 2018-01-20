using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SocialApplication.Business.ExceptionHandling;

namespace SocialApplication.Business
{
    public static class ResultBuilder
    {
        public static ObjectResult CreateErrorResult(HttpStatusCode statusCode, string message)
        {
            var errorDetails = new ErrorDetails(message);
            return new ObjectResult(errorDetails)
            {
                StatusCode = Convert.ToInt32(statusCode)
            };
        }
    }
}
