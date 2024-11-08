using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using TicketingSystem.Common.Model.DTOs.Other;

namespace TicketingSystem.NotificationService.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IMailjetClient _mailjetClient;

        public EmailService(IMailjetClient mailjetClient)
        {
            _mailjetClient = mailjetClient;
        }

        public async Task SendMail(Email email)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            var transactionalEmail = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact("yurijsok@gmail.com"))
                   .WithSubject(email.Subject)
                   .WithHtmlPart($"<h4>{email.Message}</h4>")
                   .WithTo(new SendContact(email.Address))
                   .Build();

            await _mailjetClient.SendTransactionalEmailAsync(transactionalEmail);
        }
    }
}
