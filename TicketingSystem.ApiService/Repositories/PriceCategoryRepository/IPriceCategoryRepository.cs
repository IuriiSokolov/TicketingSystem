using System.Linq.Expressions;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PriceCategoryRepository
{
    public interface IPriceCategoryRepository
    {
        Task<PriceCategory> AddAsync(PriceCategory priceCategory);
        Task<bool> DeleteAsync(int id);
        Task<List<PriceCategory>> GetAllAsync();
        Task<PriceCategory?> GetByIdAsync(int id);
        Task<List<PriceCategory>> GetWhereAsync(Expression<Func<PriceCategory, bool>> predicate);
        Task<PriceCategory> UpdateAsync(PriceCategory priceCategory);
    }
}