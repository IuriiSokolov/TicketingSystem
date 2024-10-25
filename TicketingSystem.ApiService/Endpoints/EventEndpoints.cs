using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.ApiService.Services.EventService;
using TicketingSystem.Redis;
using Microsoft.Extensions.Primitives;

namespace TicketingSystem.ApiService.Endpoints
{
    public class EventEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/events");
            eventGroup.MapGet("", GetEvents).CacheOutput(policy => policy.Expire(TimeSpan.FromSeconds(20)).Tag("GetEvents"));
            eventGroup.MapGet("{eventId}/sections/{sectionId}/seats", GetSeatsOfSectionOfEvent);
        }

        private async Task<Ok<List<TicketsFromEventAndSectionDto>>> GetSeatsOfSectionOfEvent(
            int eventId,
            int sectionId,
            IEventService service,
            IRedisCacheService cache,
            HttpContext context)
        {
            var result = await cache.GetSaveAsync(() => service.GetTicketsOfSectionOfEventAsync(eventId, sectionId),
                $"api/events/{eventId}/sections/{sectionId}/seats", TimeSpan.FromSeconds(6));
            context.Response.Headers.Append("Cache-Control", new StringValues("max-age=600"));
            return TypedResults.Ok(result);
        }

        private async Task<Ok<List<EventDto>>> GetEvents(IEventService service, HttpContext context)
        {
            context.Response.Headers.Append("Cache-Control", new StringValues("max-age=600"));
            var result = await service.GetAllAsync();
            return TypedResults.Ok(result);
        }
    }
}

