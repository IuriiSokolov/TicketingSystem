namespace TicketingSystem.Common.Model.Database
{
    public class Event
    {
        public int EventId { get; set; }
        public required string Name { get; set; }
        public required DateTime Date { get; set; }
        public string? Description { get; set; }

        public required int PlaceId { get; set; }
        public Place? Place { get; set; }

        public ICollection<Seat>? Seats { get; set; }
    }
}
