using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SectionRepository
{
    public class SectionRepository(TicketingDbContext context) : RepositoryBase<Section>(context), ISectionRepository
    {
        public async Task<List<Section>> GetWhereAsync(Expression<Func<Section, bool>> predicate)
        {
            return await Context.Sections.Where(predicate).ToListAsync();
        }
    }
}
