using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Task<bool> DeleteAsync(Guid id);
        Task<Cart?> FirstOrDefaultAsync(Expression<Func<Cart, bool>> predicate, params Expression<Func<Cart, object>>[] includes);
        Task<Cart?> GetByIdAsync(Guid id);
    }
}