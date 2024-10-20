using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        public EventService(IEventRepository eventRepository, ITicketRepository ticketRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId)
        {
            return await _ticketRepository.GetTicketsOfSectionOfEventAsync(eventId, sectionId);
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            var dtos = events.Select(e => new EventDto(e)).ToList();
            return dtos;
        }
    }
}
