using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.PostBoxs
{
    internal class PostBox : IPostBox
    {
        private string username;
        private string password;
        private string smtpServerName;
        public PostBox(IConfiguration configuration)
        {
            //smtpServerName = configuration.GetChildren().;
        }

        public void Send(MailMessage email)
        {
            SmtpClient client = new SmtpClient(smtpServerName)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            client.Send(email);
        }
    }
}
