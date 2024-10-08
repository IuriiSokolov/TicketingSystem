using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PriceCategoryRepository
{
    public class PriceCategoryRepository : IPriceCategoryRepository
    {
        private readonly TicketingDbContext _context;

        public PriceCategoryRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<PriceCategory> AddAsync(PriceCategory priceCategory)
        {
            await _context.PriceCategories.AddAsync(priceCategory);
            await _context.SaveChangesAsync();
            return priceCategory;
        }

        public async Task<PriceCategory?> GetByIdAsync(int id)
        {
            return await _context.PriceCategories.FindAsync(id);
        }

        public async Task<List<PriceCategory>> GetAllAsync()
        {
            return await _context.PriceCategories.ToListAsync();
        }

        public async Task<PriceCategory> UpdateAsync(PriceCategory priceCategory)
        {
            _context.PriceCategories.Update(priceCategory);
            await _context.SaveChangesAsync();
            return priceCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var priceCategory = await _context.PriceCategories.FindAsync(id);
            if (priceCategory == null)
            {
                return false;
            }

            _context.PriceCategories.Remove(priceCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
