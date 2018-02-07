using GeneticAlgorithmSchedule.Web.Emails.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.NullObject
{
    public class NullObjectEmailBuilder<T> : EmailBuilder<T>
    {
        public override void AddRecipient(string recipient) { }
        public override void AddRecipient(IEnumerable<string> recipients) { }
        public override void AddCC(string carbonCopie) { }
        public override void AddCC(IEnumerable<string> carbonCopies) { }
        protected override void Validate() { }

        public override async Task<Email> Build()
        {
            return await Task.Run(() =>
                {
                    Email mailMessage = new Email
                    {
                        IsBodyHtml = true,
                        Subject = "Null Object", //TODO ZMIEN NA LANGUAGE
                        Body = "Null"
                    };

                    return mailMessage;
                });
        }

    }
}
