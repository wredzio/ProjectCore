using GeneticAlgorithmSchedule.Web.Emails.EmailBuilders;
using GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.ConfirmAccount;
using GeneticAlgorithmSchedule.Web.Exceptions.ApplicationExceptions;
using GeneticAlgorithmSchedule.Web.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GeneticAlgorithmSchedule.Web.Test.Emails.EmailBuilders
{
    public class ConfirmAccountEmailBuilderTest
    {
        private EmailBuilder<ConfirmAccountTemplateModel> _emailBuilder;
        private ConfirmAccountTemplateModel _confirmAccountTemplateModel;

        public ConfirmAccountEmailBuilderTest()
        {
            _confirmAccountTemplateModel = new ConfirmAccountTemplateModel();
            var mockViewRenderService = new Mock<IViewRenderService>();
            mockViewRenderService.Setup(x => x.RenderToString("ConfirmAccountTemplate", _confirmAccountTemplateModel))
                                 .ReturnsAsync("test");

            _emailBuilder = new ConfirmAccountEmailBuilder<ConfirmAccountTemplateModel>(mockViewRenderService.Object);
        }

        [Fact]
        public async Task ShouldThrowNoRecipentEmailException()
        {
            var ex = await Assert.ThrowsAsync<EmailException>(() => _emailBuilder.Build());

            Assert.Equal("Recipients count is equals 0", ex.Message);
        }


        [Fact]
        public async Task ShouldThrowNoModelTemplateEmailException()
        {
            _emailBuilder.AddRecipient("test1@test1.pl");

            var ex = await Assert.ThrowsAsync<EmailException>(() => _emailBuilder.Build());

            Assert.Equal("TemplateModel can not be null", ex.Message);
        }

        [Fact]
        public async Task ShouldAddRecipients()
        {
            _emailBuilder.AddRecipient("test1@test1.pl");
            _emailBuilder.AddRecipient("test2@test2.pl");
            _emailBuilder.SetTemplateModel(_confirmAccountTemplateModel);
            var email = await _emailBuilder.Build();

            Assert.Equal(2, email.To.Count);
            Assert.Equal("test1@test1.pl", email.To[0]);
            Assert.Equal("test2@test2.pl", email.To[1]);
        }

        [Fact]
        public async Task ShouldReturnBody()
        {
            _emailBuilder.AddRecipient("test1@test1.pl");
            _emailBuilder.SetTemplateModel(_confirmAccountTemplateModel);
            var email = await _emailBuilder.Build();

            Assert.Equal("test", email.Body);
        }

    }
}
