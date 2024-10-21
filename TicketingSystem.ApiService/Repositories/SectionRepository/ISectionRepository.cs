using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SectionRepository
{
    public interface ISectionRepository : IRepositoryBase<Section>
    {
        Task<List<Section>> GetWhereAsync(Expression<Func<Section, bool>> predicate);
    }
}