using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.VenueRepository
{
    public class VenueRepository : IVenueRepository
    {
        TicketingDbContext _context;

        public VenueRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Venue> AddAsync(Venue venue)
        {
            await _context.Venues.AddAsync(venue);
            await _context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue?> GetByIdAsync(int id)
        {
            return await _context.Venues.FindAsync(id);
        }

        public async Task<List<Venue>> GetAllAsync()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue> UpdateAsync(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
            return venue;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return false;
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
