using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.DTOs
{
    public record struct VenueDto(int VenueId, string Name, string Address, string? Description)
    {
        public VenueDto(Venue venue) : this(venue.VenueId, venue.Name, venue.Address, venue.Description) { }
    }
}
