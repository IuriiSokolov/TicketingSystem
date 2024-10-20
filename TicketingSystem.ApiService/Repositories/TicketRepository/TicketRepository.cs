using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketingDbContext _context;

        public TicketRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return false;
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Ticket?> FirstOrDefaultAsync(Expression<Func<Ticket, bool>> predicate)
        {
            return await _context.Tickets.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TicketsFromEventAndSectionDto>> GetTicketsOfSectionOfEventAsync(int eventId, int sectionId)
        {
            var result = (from sec in _context.Sections
                          join seats in _context.Seats on sec.SectionId equals seats.SectionId
                          join t in _context.Tickets on new { seats.EventId, seats.SeatId } equals
                                                        new { t.EventId, t.SeatId }
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
