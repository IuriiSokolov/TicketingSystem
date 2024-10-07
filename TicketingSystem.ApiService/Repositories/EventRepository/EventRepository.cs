using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.EventRepository
{
    public class EventRepository : IEventRepository
    {
        TicketingDbContext _context;

        public EventRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Event> AddAsync(Event thisEvent)
        {
            await _context.Events.AddAsync(thisEvent);
            await _context.SaveChangesAsync();
            return thisEvent;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> UpdateAsync(Event thisEvent)
        {
            _context.Events.Update(thisEvent);
            await _context.SaveChangesAsync();
            return thisEvent;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var thisEvent = await _context.Events.FindAsync(id);
            if (thisEvent == null)
            {
                return false;
            }

            _context.Events.Remove(thisEvent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
