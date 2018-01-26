using GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.ConfirmAccount;
using GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.NullObject;
using GeneticAlgorithmSchedule.Web.Exceptions.ApplicationExceptions;
using GeneticAlgorithmSchedule.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders
{
    public enum EmailBuilderType
    {
        NullObject,
        ConfirmAccount
    }

    public interface IEmailBuilderFactory
    {
        EmailBuilder<T> Create<T>(EmailBuilderType emailBuilderType);
    }

    public class EmailBuilderFactory : IEmailBuilderFactory
    {
        private IViewRenderService _viewRenderService;

        public EmailBuilderFactory(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public EmailBuilder<T> Create<T>(EmailBuilderType emailBuilderType)
        {
            switch (emailBuilderType)
            {
                case EmailBuilderType.ConfirmAccount:
                    return new ConfirmAccountEmailBuilder<T>(_viewRenderService);
                case EmailBuilderType.NullObject:
                    return new NullObjectEmailBuilder<T>();
                default:
                    throw new EmailException($"{nameof(EmailBuilderFactory)} do not specify that type of {nameof(EmailBuilderType)}: {emailBuilderType.ToString()}");
            }
        }
    }
}
