using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.Common.Model.DTOs;

namespace TicketingSystem.ApiService.Endpoints
{
    public class OrderEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/orders");
            eventGroup.MapGet("carts/{cart_id}", GetTicketsInCart);
        }

        private async Task<Ok<List<TicketDto>>> GetTicketsInCart(Guid cart_id, ICartRepository repo)
        {
            var result = await repo.GetTicketsInCartAsync(cart_id);
            var dtos = result.Select(ticket => new TicketDto(ticket)).ToList();
            return TypedResults.Ok(dtos);
        }
    }
}
