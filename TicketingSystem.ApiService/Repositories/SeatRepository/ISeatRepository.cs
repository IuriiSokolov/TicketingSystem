using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.SeatRepository
{
    public interface ISeatRepository
    {
        Task<Seat> AddAsync(Seat seat);
        Task<bool> DeleteAsync(int id);
        Task<List<Seat>> GetAllAsync();
        Task<Seat?> GetByIdAsync(int id);
        Task<Seat> UpdateAsync(Seat seat);
    }
}