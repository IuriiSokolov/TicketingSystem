using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SectionRepository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly TicketingDbContext _context;

        public SectionRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Section> AddAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return section;
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            return await _context.Sections.FindAsync(id);
        }

        public async Task<List<Section>> GetAllAsync()
        {
            return await _context.Sections.ToListAsync();
        }

        public async Task<Section> UpdateAsync(Section section)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync();
            return section;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return false;
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Section>> GetWhereAsync(Expression<Func<Section, bool>> predicate)
        {
            return await _context.Sections.Where(predicate).ToListAsync();
        }
    }
}
