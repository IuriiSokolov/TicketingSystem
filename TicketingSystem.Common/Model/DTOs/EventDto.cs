using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.DTOs
{
    public record struct EventDto(int EventId, string Name, DateTime Date, string? description)
    {
        public EventDto(Event thisEvent): this(thisEvent.EventId, thisEvent.Name, thisEvent.Date, thisEvent.Description) { }
    }
}
