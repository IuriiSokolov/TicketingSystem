using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.ApiService.Services.EventService;
using TicketingSystem.Redis;

namespace TicketingSystem.ApiService.Endpoints
{
    public class EventEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/events");
            eventGroup.MapGet("", GetEvents).CacheOutput(policy => policy.Expire(TimeSpan.FromSeconds(5)));
            eventGroup.MapGet("{eventId}/sections/{sectionId}/seats", GetSeatsOfSectionOfEvent);
        }

        private async Task<Ok<List<TicketsFromEventAndSectionDto>>> GetSeatsOfSectionOfEvent(int eventId, int sectionId, IEventService service, IRedisCacheService cache)
        {
            var result = await cache.GetSaveAsync(() => service.GetTicketsOfSectionOfEventAsync(eventId, sectionId),
                $"api/events/{eventId}/sections/{sectionId}/seats", TimeSpan.FromSeconds(6));
            return TypedResults.Ok(result);
        }

        private async Task<Ok<List<EventDto>>> GetEvents(IEventService service, IRedisCacheService cache)
        {
            var result = await cache.GetSaveAsync(service.GetAllAsync, "api/events");
            return TypedResults.Ok(result);
        }
    }
}

