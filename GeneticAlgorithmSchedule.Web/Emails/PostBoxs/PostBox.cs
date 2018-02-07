using AutoMapper;
using GeneticAlgorithmSchedule.Web.Emails.Model;
using Hangfire;
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

        [AutomaticRetry(Attempts = 6)]
        public void Send(Email email)
        {
            var mailMessage = MapToMailMessage(email);

            SmtpClient client = new SmtpClient(_smtpServerName)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _password),
                Port = 587,
                EnableSsl = true
            };

            using (client)
            {
                client.Send(mailMessage);
            }
        }

        public void ChangeEmailAdress(string username, string password)
        {
            _username = username;
            _password = password;
        }

        private MailMessage MapToMailMessage(Email email)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(string.Join(",", email.To));
            mailMessage.Subject = email.Subject;
            mailMessage.From = new MailAddress(_username);
            mailMessage.IsBodyHtml = email.IsBodyHtml;
            mailMessage.Body = email.Body;

            if (email.CC.Count != 0)
            {
                mailMessage.CC.Add(string.Join(",", email.CC));
            }

            return mailMessage;
        }
    }
}
