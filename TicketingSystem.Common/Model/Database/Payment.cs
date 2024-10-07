namespace TicketingSystem.Common.Model.Database
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public required DateTime PaymentTime { get; set; }

        public Ticket Ticket { get; set; } = null!;
    }
}
