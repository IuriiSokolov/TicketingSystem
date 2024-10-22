using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.VenueService
{
    public interface IVenueService
    {
        Task<List<VenueDto>> GetAllAsync();
        Task<List<SectionDto>?> GetSectionsAsync(int venueId);
    }
}