using TicketingSystem.ApiService.Repositories.EventRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.DTOs.Output;

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

        private async Task<Ok<List<TicketsFromEventAndSectionDto>>> GetSeatsOfSectionOfEvent(int event_id, int section_id, IEventRepository repo)
        {
            var result = await repo.GetTicketsOfSectionOfEventAsync(event_id, section_id);
            return TypedResults.Ok(result);
        }

        private async Task<Ok<List<EventDto>>> GetEvents(IEventRepository repo)
        {
            var events = await repo.GetAllAsync();
            var dtos = events.Select(thisEvent => new EventDto(thisEvent)).ToList();
            return TypedResults.Ok(dtos);
        }
    }
}

