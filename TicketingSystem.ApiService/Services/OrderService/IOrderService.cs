using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.OrderService
{
    public interface IOrderService
    {
        Task<CartDto?> AddTicketToCartAsync(Guid cartId, int eventId, int seatId);
        Task<PaymentDto?> BookTicketsInCart(Guid cartId);
        Task<List<TicketDto>> GetTicketsInCartAsync(Guid cartId);
        Task<bool> RemoveTicketFromCartAsync(Guid cartId, int eventId, int seatId);
    }
}