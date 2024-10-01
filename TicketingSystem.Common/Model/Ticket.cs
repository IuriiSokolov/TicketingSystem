namespace TicketingSystem.Common.Model
{
    public class Ticket
    {
        public required int Id { get; set; }
        public required Seat Seat { get; set; }
        public required Person Person { get; set; }
    }
}
