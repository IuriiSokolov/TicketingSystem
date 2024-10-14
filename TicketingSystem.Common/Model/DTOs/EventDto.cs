using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.DTOs
{
    public class EventDto
    {
        public required string Name { get; set; }
        public required DateTime Date { get; set; }
        public string? Description { get; set; }

        public required int VenueId { get; set; }

        public Event ToEvent()
        {
            return new Event
            {
                Date = Date,
                Name = Name,
                Description = Description,
                VenueId = VenueId
            };
        }
    }
}
