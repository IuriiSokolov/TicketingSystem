namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Section
    {
        public int SectionId { get; set; }

        public required int VenueId { get; set; }
        public Venue? Venue { get; set; }

        public ICollection<Seat> Seats { get; set; } = [];
    }
}
