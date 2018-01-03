using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders
{
    public class ConfirmAccountEmailBuilder : EmailBuilder
    {
        public override MailMessage Build()
        {
            Validate();

            MailMessage mailMessage = new MailMessage();

            foreach(var carbonCopie in _carbonCopies)
            {
                mailMessage.CC.Add(carbonCopie);
            }

            foreach(var _recipient in _recipients)
            {
                mailMessage.To.Add(_recipient);
            }

            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Confirm Account"; //TODO ZMIEN NA LANGUAGE
            mailMessage.Body = "XX";

            return mailMessage;
        }
    }
}
