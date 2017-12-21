using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; protected set; } = HttpStatusCode.InternalServerError;
        public object Model { get; set; } = null;

        public CustomException()
        { }

        public CustomException(string message)
            : base(message)
        { }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CustomException(string message, Exception innerException, HttpStatusCode httpStatusCode)
           : this(message, innerException, httpStatusCode, null)
        { }

        public CustomException(string message, Exception innerException, HttpStatusCode httpStatusCode, object model)
          : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
            Model = model;
        }
    }
}
