using MassTransit;
using TicketingSystem.Common.Model.DTOs.Other;
using TicketingSystem.NotificationService.EmailService;

namespace TicketingSystem.NotificationService
{
    public class EmailConsumer : IConsumer<Email>
    {
        private readonly IEmailService _emailService;

        public EmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Consume(ConsumeContext<Email> context)
        {
            await _emailService.SendMail(context.Message);
        }
    }
}
