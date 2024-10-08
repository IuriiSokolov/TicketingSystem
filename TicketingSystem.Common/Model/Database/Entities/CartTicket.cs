namespace TicketingSystem.Common.Model.Database.Entities
{
    public class CartTicket
    {
        public int CartId { get; set; }
        public int TicketId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}
