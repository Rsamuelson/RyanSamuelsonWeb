using Application.Emails.Interfaces;
using FluentAssertions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Email
{
    public class SendGridEmailService : IEmailService
    {
        private const string ApiKey = "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";

        public async Task<HttpStatusCode> SendEmail(string to, string from, string message)
        {
            var client = new SendGridClient(ApiKey);

            var fromEmailAddress = new EmailAddress("2015ryansamuelson@gmail.com");
            var subject = "Email From SendGrid";
            var toEmailAddress = new EmailAddress("2015ryansamuelson@gmail.com");

            var msg = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, subject, message, message);

            var result = await client.SendEmailAsync(msg);
            return result.StatusCode;
        }


    }

    public class SendGridEmailServiceTests
    {
        private readonly SendGridEmailService _sendGridEmailService;

        public SendGridEmailServiceTests()
        {
            _sendGridEmailService = new SendGridEmailService();
        }

        [Fact(Skip = "TODO")]
        public async Task whenCalled_emailSentToMeAsync() =>
            (await _sendGridEmailService.SendEmail("2015RyanSamuelson@gmail.com", "2015RyanSamuelson@gmail.com", "Unit Test message from RyanSamuelsonWeb"))
                .Should().Be(HttpStatusCode.Accepted);
    }
}
