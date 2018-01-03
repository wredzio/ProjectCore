using GeneticAlgorithmSchedule.Web.Exceptions.ApplicationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders
{
    public abstract class EmailBuilder
    {
        protected List<string> _recipients;
        protected List<string> _carbonCopies;

        public virtual void AddRecipient(string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                throw new EmailException("Recipient is null or empty");
            }

            _recipients.Add(recipient);
        }

        public virtual void AddRecipient(IEnumerable<string> recipients)
        {
            if (!recipients.Any())
            {
                throw new EmailException("Recipients count is equals 0");
            }

            _recipients.AddRange(recipients);
        }

        public virtual void AddCC(string carbonCopie)
        {
            if (string.IsNullOrEmpty(carbonCopie))
            {
                throw new EmailException("carbonCopie is null or empty");
            }

            _carbonCopies.Add(carbonCopie);
        }

        public virtual void AddCC(IEnumerable<string> carbonCopies)
        {
            if (!carbonCopies.Any())
            {
                throw new EmailException("carbonCopies count is equals 0");
            }

            _carbonCopies.AddRange(carbonCopies);
        }

        protected virtual void Validate()
        {
            if(!_recipients.Any())
            {
                throw new EmailException("Recipients count is equals 0");
            }
        }

        public abstract MailMessage Build();
    }
}
