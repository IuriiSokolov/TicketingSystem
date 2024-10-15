using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart);
        Task<(Cart?, float TotalPriceUsd)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId);
        Task<Payment?> BookTicketsInCart(Guid cartId);
        Task<bool> DeleteAsync(int id);
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<List<Ticket>> GetTicketsInCartAsync(Guid cartId);
        Task<bool> RemoveTicketFromCartAsync(Guid cartId, int eventId, int seatId);
        Task<Cart> UpdateAsync(Cart cart);
    }
}