using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.OrderService
{
    public interface IOrderService
    {
        Task<(CartDto? Dto, string? ErrorMsg)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId);
        Task<PaymentDto?> BookTicketsInCart(Guid cartId);
        Task<List<TicketDto>> GetTicketsInCartAsync(Guid cartId);
        Task<string?> RemoveTicketFromCartAsync(Guid cartId, int eventId, int seatId);
    }
}