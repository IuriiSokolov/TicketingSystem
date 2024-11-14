
using TicketingSystem.Common.Model.DTOs.Other;

namespace TicketingSystem.NotificationService.EmailService
{
    public interface IEmailService
    {
        Task SendMail(Email email);
    }
}