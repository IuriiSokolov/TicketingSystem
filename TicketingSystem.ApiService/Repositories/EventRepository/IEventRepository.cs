using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Repositories.EventRepository
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event thisEvent);
        Task<bool> DeleteAsync(int id);
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<Event> UpdateAsync(Event thisEvent);
    }
}
