using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException()
        { }

        public BadRequestException(string message) : base(message)
        { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        { }

        public BadRequestException(string message, Exception innerException, object model) 
            : base(message, innerException, HttpStatusCode.BadRequest, model)
        { }
    }
}
