using Application.Emails.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Emails.Notifications
{
    public record SendEmailNotification : INotification
    {
        public SendEmailNotification(string to, string from, string message)
        {
            To = to;
            From = from;
            Message = message;
        }

        private string To { get; }
        private string From { get; }
        private string Message { get; }

        public sealed class SendEmailNotificationHandler : INotificationHandler<SendEmailNotification>
        {
            private readonly IEmailService _emailService;

            public SendEmailNotificationHandler(IEmailService emailService)
            {
                _emailService = emailService;
            }

            public async Task Handle(SendEmailNotification notification, CancellationToken cancellationToken = default)
            {
                await _emailService.SendEmail(notification.To, notification.From, notification.Message);
            }
        }
    }
}
