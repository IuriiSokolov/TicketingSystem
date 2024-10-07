using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> UpdateAsync(Ticket ticket);
    }
}