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
            var orderGroup = app.MapGroup("api/orders");
            orderGroup.MapGet("carts/{cart_id}", GetTicketsInCart);
            orderGroup.MapPost("carts/{cart_id}", AddTicketToCart);
            orderGroup.MapDelete("carts/{cart_id}/events/{event_id}/seats/{seat_id}", RemoveTicketFromCart);
        }

        private async Task<Ok<List<TicketDto>>> GetTicketsInCart(Guid cart_id, ICartRepository repo)
        {
            var result = await repo.GetTicketsInCartAsync(cart_id);
            var dtos = result.Select(ticket => new TicketDto(ticket)).ToList();
            return TypedResults.Ok(dtos);
        }

        private async Task<Results<Ok<CartDto>, NotFound>> AddTicketToCart(Guid cart_id, AddTicketToCartDto dto, ICartRepository repo)
        {
            var (cart, totalPriceUsd) = await repo.AddTicketToCartAsync(cart_id, dto.EventId, dto.SeatId);
            if (cart == null)
                return TypedResults.NotFound();

            var resultDto = new CartDto(cart, totalPriceUsd);
            return TypedResults.Ok(resultDto);
        }

        private async Task<Results<Ok, NotFound>> RemoveTicketFromCart(Guid cart_id, int event_id, int seat_id, ICartRepository repo)
        {
            var result = await repo.RemoveTicketFromCartAsync(cart_id, event_id, seat_id);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }
    }
}
