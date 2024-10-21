using System.Linq.Expressions;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SectionRepository
{
    public interface ISectionRepository
    {
        Task<Section> AddAsync(Section section);
        Task<bool> DeleteAsync(int id);
        Task<List<Section>> GetAllAsync();
        Task<Section?> GetByIdAsync(int id);
        Task<List<Section>> GetWhereAsync(Expression<Func<Section, bool>> predicate);
        Task<Section> UpdateAsync(Section section);
    }
}