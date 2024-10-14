using TicketingSystem.ApiService.Repositories.EventRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs;

namespace TicketingSystem.ApiService.Endpoints
{
    public class EventEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("api/events");
            userGroup.MapGet("", GetEvents);
            userGroup.MapGet("id", GetEvent);
            userGroup.MapGet("{event_id}/sections/{section_id}/seats", GetSeatsOfSectionOfEvent);
            //userGroup.MapGet("/events/{event_id}/sections/{section_id}/seats", async (int event_id, int section_id, IEventRepository repo) =>
            //{
            //    var seats = await repo.GetSeatsOfSectionOfEventAsync();

            //});
            //userGroup.MapPost("", AddEvent);
            //userGroup.MapPut("", UpdateEvent);
            //userGroup.MapDelete("", DeleteEvent);
        }

        private async Task<List<TicketsFromEventAndSectionDto>> GetSeatsOfSectionOfEvent(int event_id, int section_id, IEventRepository repo)
        {
            return await repo.GetTicketsOfSectionOfEventAsync(event_id, section_id);
        }

        private async Task<Ok<List<Event>>> GetEvents(IEventRepository repo)
        {
            var dtos = await repo.GetAllAsync();
            return TypedResults.Ok(dtos);
        }

        private async Task<Ok<Event>> GetEvent(IEventRepository repo, int id)
        {
            var dto = await repo.GetByIdAsync(id);
            return TypedResults.Ok(dto);
        }

        private async Task<Results<Ok, BadRequest<string>>> AddEvent(IEventRepository repo, EventDto dto)
        {
            if (dto == null)
            {
                return TypedResults.BadRequest("Event was null");
            }
            try
            {
                await repo.AddAsync(dto.ToEvent());
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }

        private async Task<Results<Ok, BadRequest<string>>> UpdateEvent(IEventRepository repo, Event dto)
        {
            if (dto == null)
            {
                return TypedResults.BadRequest("Event was null");
            }
            try
            {
                await repo.UpdateAsync(dto);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }

        private async Task<Results<Ok, BadRequest<string>>> DeleteEvent(IEventRepository repo, int id)
        {
            if (id < 0)
            {
                return TypedResults.BadRequest("Id was not valid");
            }
            try
            {
                await repo.DeleteAsync(id);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }
    }
}

