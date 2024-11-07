using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

namespace TicketingSystem.NotificationService.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IMailjetClient _mailjetClient;

        public EmailService(IMailjetClient mailjetClient)
        {
            _mailjetClient = mailjetClient;
        }

        public async Task SendMail(string message)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact("yurijsok@gmail.com"))
                   .WithSubject("Test subject")
                   .WithHtmlPart($"<h4>{message}</h4>")
                   .WithTo(new SendContact("yurijsok@gmail.com"))
                   .Build();

            // invoke API to send email
            var responce = await _mailjetClient.SendTransactionalEmailAsync(email);
        }
    }
}
