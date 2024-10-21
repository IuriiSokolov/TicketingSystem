using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.DTOs.Output
{
    public record struct SectionDto(int SectionId, int VenueId)
    {
        public SectionDto(Section section) : this(section.SectionId, section.VenueId) { }
    }
}
