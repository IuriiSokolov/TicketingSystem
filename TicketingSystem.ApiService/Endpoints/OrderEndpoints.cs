using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Services.OrderService;
using TicketingSystem.Common.Model.DTOs.Input;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Endpoints
{
    public class OrderEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var orderGroup = app.MapGroup("api/orders/carts");
            orderGroup.MapGet("{cart_id}", GetTicketsInCart);
            orderGroup.MapPost("{cart_id}", AddTicketToCart);
            orderGroup.MapDelete("{cart_id}/events/{event_id}/seats/{seat_id}", RemoveTicketFromCart);
            orderGroup.MapPut("{cart_id}/book", BookTicketsInCart);
        }

        private async Task<Ok<List<TicketDto>>> GetTicketsInCart(Guid cart_id, IOrderService service)
        {
            var result = await service.GetTicketsInCartAsync(cart_id);
            return TypedResults.Ok(result);
        }

        private async Task<Results<Ok<CartDto>, NotFound>> AddTicketToCart(Guid cart_id, AddTicketToCartDto dto, IOrderService service)
        {
            var resultDto = await service.AddTicketToCartAsync(cart_id, dto.EventId, dto.SeatId);

            return resultDto is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(resultDto.Value);
        }

        private async Task<Results<Ok, NotFound>> RemoveTicketFromCart(Guid cart_id, int event_id, int seat_id, IOrderService service)
        {
            var result = await service.RemoveTicketFromCartAsync(cart_id, event_id, seat_id);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }
        private async Task<Results<Ok<PaymentDto>, NotFound>> BookTicketsInCart(Guid cart_id, IOrderService service)
        {
            var dto = await service.BookTicketsInCart(cart_id);
            return dto is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(dto.Value);
        }
    }
}
