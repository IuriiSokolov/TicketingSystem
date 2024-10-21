using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PriceCategoryRepository
{
    public class PriceCategoryRepository(TicketingDbContext context) : RepositoryBase<PriceCategory>(context), IPriceCategoryRepository
    {
        public async Task<List<PriceCategory>> GetWhereAsync(Expression<Func<PriceCategory, bool>> predicate)
        {
            return await Context.PriceCategories.Where(predicate).ToListAsync();
        }
    }
}
