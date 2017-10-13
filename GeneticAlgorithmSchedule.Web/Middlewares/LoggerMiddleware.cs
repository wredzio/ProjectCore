using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<Controller> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<Controller> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                _logger.LogInformation("Koty");
                await _next(context);
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
