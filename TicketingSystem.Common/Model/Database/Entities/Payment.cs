using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public required PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DateTime? PaymentTime { get; set; }

        public Cart? Cart { get; set; }
    }
}
