using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.VenueRepository
{
    public interface IVenueRepository
    {
        Task<Venue> AddAsync(Venue venue);
        Task<bool> DeleteAsync(int id);
        Task<List<Venue>> GetAllAsync();
        Task<Venue?> GetByIdAsync(int id);
        Task<Venue> UpdateAsync(Venue venue);
    }
}
