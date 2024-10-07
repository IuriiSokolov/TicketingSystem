namespace TicketingSystem.Common.Model.Database
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public required int SeatId { get; set; }
        public Seat? Seat { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
