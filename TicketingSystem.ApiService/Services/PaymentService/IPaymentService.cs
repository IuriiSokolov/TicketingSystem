using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.ApiService.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<bool> CompletePayment(int paymentId);
        Task<bool> FailPayment(int paymentId);
        Task<PaymentStatus?> GetStatusByIdAsync(int paymentId);
    }
}