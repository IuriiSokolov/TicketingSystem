using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PriceCategoryRepository
{
    public interface IPriceCategoryRepository : IRepositoryBase<PriceCategory>
    {
        Task<List<PriceCategory>> GetWhereAsync(Expression<Func<PriceCategory, bool>> predicate);
    }
}