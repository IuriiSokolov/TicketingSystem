using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.Common.Model.DTOs.Input;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Endpoints
{
    public class OrderEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var eventGroup = app.MapGroup("api/orders");
            eventGroup.MapGet("carts/{cart_id}", GetTicketsInCart);
            eventGroup.MapPost("carts/{cart_id}", AddTicketToCart);
        }

        private async Task<Results<Ok<CartDto>, NotFound, BadRequest<string>>> AddTicketToCart(Guid cart_id, AddTicketToCartDto dto, ICartRepository repo)
        {
            var (cart, totalPriceUsd) = await repo.AddTicketToCartAsync(cart_id, dto.EventId, dto.SeatId);
            if (cart == null)
                return TypedResults.NotFound();

            var resultDto = new CartDto(cart, totalPriceUsd);
            return TypedResults.Ok(resultDto);
        }

        private async Task<Ok<List<TicketDto>>> GetTicketsInCart(Guid cart_id, ICartRepository repo)
        {
            var result = await repo.GetTicketsInCartAsync(cart_id);
            var dtos = result.Select(ticket => new TicketDto(ticket)).ToList();
            return TypedResults.Ok(dtos);
        }
    }
}
