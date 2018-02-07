using GeneticAlgorithmSchedule.Web.Emails.Model;
using GeneticAlgorithmSchedule.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.ConfirmAccount
{
    public class ConfirmAccountEmailBuilder<T> : EmailBuilder<T>
    {
        private IViewRenderService _viewRenderService;

        public ConfirmAccountEmailBuilder(IViewRenderService viewRenderService) : base()
        {
            _viewRenderService = viewRenderService;
        }

        public override async Task<Email> Build()
        {
            var mailMessage = await base.Build();

            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Confirm Account"; //TODO ZMIEN NA LANGUAGE
            mailMessage.Body = await _viewRenderService.RenderToString("~/Emails/EmailBuilders/ConfirmAccount/ConfirmAccountTemplate.cshtml", _templateModel);

            return mailMessage;
        }
    }
}
