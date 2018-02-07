using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.Model
{
    public class Email
    {
        public string From { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
    }
}
