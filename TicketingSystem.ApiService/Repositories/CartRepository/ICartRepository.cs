using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart);
        Task<bool> DeleteAsync(int id);
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart> UpdateAsync(Cart cart);
    }
}