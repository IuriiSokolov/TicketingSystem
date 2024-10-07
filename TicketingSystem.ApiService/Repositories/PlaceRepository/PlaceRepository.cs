using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.PlaceRepository
{
    public class PlaceRepository : IPlaceRepository
    {
        TicketingDbContext _context;

        public PlaceRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Place> AddAsync(Place place)
        {
            await _context.Places.AddAsync(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<Place?> GetByIdAsync(int id)
        {
            return await _context.Places.FindAsync(id);
        }

        public async Task<List<Place>> GetAllAsync()
        {
            return await _context.Places.ToListAsync();
        }

        public async Task<Place> UpdateAsync(Place place)
        {
            _context.Places.Update(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return false;
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
