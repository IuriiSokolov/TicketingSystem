namespace TicketingSystem.Common.Model.Database
{
    public class Venue
    {
        public int VenueId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? Description { get; set; }

        public ICollection<Event>? Events { get; }
    }
}
