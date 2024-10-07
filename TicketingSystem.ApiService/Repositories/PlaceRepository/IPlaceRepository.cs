using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.PlaceRepository
{
    public interface IPlaceRepository
    {
        Task<Place> AddAsync(Place place);
        Task<bool> DeleteAsync(int id);
        Task<List<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(int id);
        Task<Place> UpdateAsync(Place place);
    }
}
