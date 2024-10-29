using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.ApiService.Services.EventService;
using TicketingSystem.ApiService.Cache;

namespace TicketingSystem.ApiService.Endpoints
{
    public class EventEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/events");
            eventGroup.MapGet("", GetEvents).CacheOutput(policy => policy.Expire(TimeSpan.FromSeconds(20)).Tag(CacheTags.GetEvents));
            eventGroup.MapGet("{eventId}/sections/{sectionId}/seats", GetSeatsOfSectionOfEvent);
        }

        private async Task<Ok<List<TicketsFromEventAndSectionDto>>> GetSeatsOfSectionOfEvent(
            int eventId,
            int sectionId,
            IEventService service,
            HttpContext context)
        {
            var result = await service.GetTicketsOfSectionOfEventAsync(eventId, sectionId);
            return TypedResults.Ok(result);
        }

        private async Task<Ok<List<EventDto>>> GetEvents(IEventService service, HttpContext context)
        {
            var result = await service.GetAllAsync();
            return TypedResults.Ok(result);
        }
    }
}

