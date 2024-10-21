using System.Linq.Expressions;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart);
        Task<bool> DeleteAsync(Guid id);
        Task<Cart?> FirstOrDefaultWithTicketsAsync(Expression<Func<Cart, bool>> predicate);
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(Guid id);
        Task<List<Ticket>> GetTicketsInCartAsync(Guid cartId);
        Task<Cart> UpdateAsync(Cart cart);
    }
}