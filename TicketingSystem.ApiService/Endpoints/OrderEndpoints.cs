using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Services.OrderService;
using TicketingSystem.Common.Model.DTOs.Input;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.Redis;

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

        private async Task<Results<Ok<CartDto>, NotFound<string>>> AddTicketToCart(Guid cart_id, AddTicketToCartDto dto, IOrderService service, IRedisCacheService cache)
        {
            var (resultDto, errorMsg) = await service.AddTicketToCartAsync(cart_id, dto.EventId, dto.SeatId);

            return resultDto is null
                ? TypedResults.NotFound(errorMsg)
                : TypedResults.Ok(resultDto.Value);
        }

        private async Task<Results<Ok, NotFound<string>>> RemoveTicketFromCart(Guid cart_id, int event_id, int seat_id, IOrderService service)
        {
            var error = await service.RemoveTicketFromCartAsync(cart_id, event_id, seat_id);
            return error is null
                ? TypedResults.Ok()
                : TypedResults.NotFound(error);
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
