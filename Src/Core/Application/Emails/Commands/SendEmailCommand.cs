using Application.Emails.Notifications;
using Common.TestUtilities;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Emails.Commands
{
    public record SendEmailCommand : IRequest
    {
        public SendEmailCommand(SendEmailViewModel sendEmailViewModel)
        {
            ViewModel = sendEmailViewModel;
        }

        internal SendEmailViewModel ViewModel { get; }

        internal sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
        {
            private readonly IPublisher _publisher;

            public SendEmailCommandHandler(IPublisher publisher)
            {
                _publisher = publisher;
            }

            public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken = default)
            {
                await _publisher.Publish(new SendEmailNotification("2015RyanSamuelson@gmail.com", request.ViewModel.From, request.ViewModel.Message), cancellationToken);
                return Unit.Value;
            }
        }
    }

    public class SendEmailCommandHandlerTests
    {
        private readonly Mock<IPublisher> _mockPublisher;
        private readonly SendEmailCommand.SendEmailCommandHandler _handler;

        public SendEmailCommandHandlerTests()
        {
            _mockPublisher = new Mock<IPublisher>();
            _handler = new SendEmailCommand.SendEmailCommandHandler(_mockPublisher.Object);
        }

        [Fact]
        public async Task PublisherPublishSendEmailNotificationIsCalledOnce()
        {
            var testSendEmailViewModel = new SendEmailViewModel()
            {
                From = RandomStringUtilities.RandomAlphaNumericString(10),
                Message = RandomStringUtilities.RandomAlphaNumericString(10)
            };
            await _handler.Handle(new SendEmailCommand(testSendEmailViewModel));

            _mockPublisher.Verify(p => p.Publish(new SendEmailNotification("2015RyanSamuelson@gmail.com", testSendEmailViewModel.From, testSendEmailViewModel.Message), It.IsAny<CancellationToken>()));
        }
    }
}
