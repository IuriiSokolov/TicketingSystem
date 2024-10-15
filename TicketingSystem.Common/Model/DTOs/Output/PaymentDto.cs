using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;
namespace TicketingSystem.Common.Model.DTOs.Output
{
    public record struct PaymentDto(int PaymentId, PaymentStatus PaymentStatus, DateTime? PaymentTime)
    {
        public PaymentDto(Payment payment) : this(payment.PaymentId, payment.PaymentStatus, payment.PaymentTime) { }
    }
}
