
namespace TicketingSystem.NotificationService.EmailService
{
    public interface IEmailService
    {
        Task SendMail(string message);
    }
}