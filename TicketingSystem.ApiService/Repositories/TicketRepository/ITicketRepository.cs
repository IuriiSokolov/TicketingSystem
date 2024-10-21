using System.Linq.Expressions;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);
        Task<Ticket?> FirstOrDefaultAsync(Expression<Func<Ticket, bool>> predicate);
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<List<Ticket>> GetTicketsInCartAsync(Guid cartId);
        Task<List<Ticket>> GetWhereAsync(Expression<Func<Ticket, bool>> predicate, params Expression<Func<Ticket, object>>[] includes);
        Task<Ticket> UpdateAsync(Ticket ticket);
    }
}