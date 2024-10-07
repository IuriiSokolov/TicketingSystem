namespace TicketingSystem.Common.Model.Database
{
    public class Cart
    {
        public int CartId { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<Ticket> Tickets { get; } = [];
    }
}
