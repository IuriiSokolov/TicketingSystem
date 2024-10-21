using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.EventService
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId);
    }
}