using GeneticAlgorithmSchedule.Web.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomException customException)
            {
                await HandleExceptionAsync(context, customException);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            object model = null;

            if (exception is CustomException)
            {
                var customException = exception as CustomException;
                code = customException.HttpStatusCode;
                model = customException.Model;
            }

            var result = JsonConvert.SerializeObject(new { errorMessage = code == HttpStatusCode.InternalServerError? "Internal Server Error" : exception.Message, model });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
