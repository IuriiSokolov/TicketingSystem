using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Repositories.EventRepository
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketingDbContext _context;

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

        public async Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId)
        {
            var result = (from sec in _context.Sections
                          join seats in _context.Seats on sec.SectionId equals seats.SectionId
                          join t in _context.Tickets on seats.EventId equals t.EventId
                          join pc in _context.PriceCategories on t.PriceCategoryId equals pc.PriceCategoryId
                          where t.EventId == eventId && sec.SectionId == sectionId
                          select new TicketsFromEventAndSectionDto
                          {
                              SectionId = seats.SectionId!.Value,
                              RowNumber = seats.RowNumber,
                              SeatId = seats.SeatId,
                              SeatStatus = t.Status,
                              PriceCategoryId = pc.PriceCategoryId,
                              PriceCategoryName = pc.PriceCategoryName,
                          });
            return await result.ToListAsync();

        }
    }
}
