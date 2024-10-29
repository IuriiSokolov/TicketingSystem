using TicketingSystem.ApiService.Cache;
using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.Redis;

namespace TicketingSystem.ApiService.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IRedisCacheService _cache;
        public EventService(IEventRepository eventRepository, ITicketRepository ticketRepository, IRedisCacheService cache)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _cache = cache;
        }

        public async Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId)
        {
            return await _cache.GetSaveAsync(() => GetTicketsOfSectionOfEventNonCachedAsync(eventId, sectionId),
                CacheKeys.GetSeatsOfSectionOfEvent(eventId, sectionId), TimeSpan.FromSeconds(6));
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            var dtos = events.Select(e => new EventDto(e)).ToList();
            return dtos;
        }

        private async Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventNonCachedAsync(int eventId, int sectionId)
        {
            var tickets = await _ticketRepository.GetWhereAsync(t => t.EventId == eventId && t.Seat!.SectionId == sectionId,
                t => t.Seat!, t => t.PriceCategory!);
            var dtos = tickets.Select(ticket => new TicketsFromEventAndSectionDto
            {
                SectionId = ticket.Seat!.SectionId!.Value,
                RowNumber = ticket.Seat!.RowNumber,
                SeatId = ticket.SeatId,
                SeatStatus = ticket.Status,
                PriceCategoryId = ticket.PriceCategoryId,
                PriceCategoryName = ticket.PriceCategory!.PriceCategoryName
            }).ToList();
            return dtos;
        }
    }
}
