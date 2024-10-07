namespace TicketingSystem.Common.Model.Database
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public required float PriceUsd { get; set; }

        public Seat? Seat { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<Cart> Carts { get; set; } = [];
    }
}
