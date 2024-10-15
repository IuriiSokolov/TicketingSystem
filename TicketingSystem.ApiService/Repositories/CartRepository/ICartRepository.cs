using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart);
        Task<(Cart?, float TotalPriceUsd)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId);
        Task<bool> DeleteAsync(int id);
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<List<Ticket>> GetTicketsInCartAsync(Guid cartId);
        Task<Cart> UpdateAsync(Cart cart);
    }
}