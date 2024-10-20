using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.ApiService.Services.EventService;

namespace TicketingSystem.ApiService.Endpoints
{
    public class EventEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/events");
            eventGroup.MapGet("", GetEvents);
            eventGroup.MapGet("{event_id}/sections/{section_id}/seats", GetSeatsOfSectionOfEvent);
        }

        private async Task<Ok<List<TicketsFromEventAndSectionDto>>> GetSeatsOfSectionOfEvent(int event_id, int section_id, IEventService service)
        {
            var result = await service.GetTicketsOfSectionOfEventAsync(event_id, section_id);
            return TypedResults.Ok(result);
        }

        private async Task<Ok<List<EventDto>>> GetEvents(IEventService service)
        {
            var result = await service.GetAllAsync();
            return TypedResults.Ok(result);
        }
    }
}

