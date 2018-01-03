using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Exceptions.ApplicationExceptions
{
    public class EmailException : ApplicationException
    {
        public EmailException()
        {
        }

        public EmailException(string message) : base(message)
        {
        }

        public EmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
