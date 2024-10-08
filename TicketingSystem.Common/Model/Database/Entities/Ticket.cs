namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public required int PriceCategoryId { get; set; }
        public PriceCategory? PriceCategory { get; set; }

        public required int EventId { get; set; }
        public Event? Event { get; set; }

        public required int SeatId { get; set; }
        public Seat? Seat { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<Cart> Carts { get; set; } = [];
    }
}
