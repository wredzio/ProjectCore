using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.PostBoxs
{
    public interface IPostBox
    {
        void Send(MailMessage email);
    }
}
