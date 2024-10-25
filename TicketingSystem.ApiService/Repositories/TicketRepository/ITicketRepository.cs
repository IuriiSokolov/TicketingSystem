using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public interface ITicketRepository : IRepositoryBase<Ticket>
    {
        Task<Ticket?> FirstOrDefaultAsync(Expression<Func<Ticket, bool>> predicate, params Expression<Func<Ticket, object>>[] includes);
        Task<List<Ticket>> GetWhereAsync(Expression<Func<Ticket, bool>> predicate, params Expression<Func<Ticket, object>>[] includes);
    }
}