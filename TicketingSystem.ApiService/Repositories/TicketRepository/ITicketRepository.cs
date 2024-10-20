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
        Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId);
        Task<Ticket> UpdateAsync(Ticket ticket);
    }
}