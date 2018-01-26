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
        private string _username;
        private string _password;
        private string _smtpServerName;

        public PostBox(IConfiguration configuration)
        {
            _smtpServerName = configuration["STMP:ServerName"];
            _username = configuration["STMP:Username"];
            _password = configuration["STMP:Password"];
        }

        public void Send(MailMessage email)
        {
            email.From = new MailAddress(_username);

            SmtpClient client = new SmtpClient(_smtpServerName)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _password),
                Port = 587,
                EnableSsl = true
            };

            client.Send(email);
        }

        public void ChangeEmailAdress(string username, string password)
        {
            _username = username;
            _password = password;
        }
    }
}
