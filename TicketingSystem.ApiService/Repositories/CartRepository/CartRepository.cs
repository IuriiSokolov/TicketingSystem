using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly TicketingDbContext _context;

        public CartRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return false;
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ticket>> GetTicketsInCartAsync(Guid cartId)
        {
            var result = await _context.Tickets.Where(ticket => ticket.CartId == cartId).ToListAsync();
            return result;
        }
    }
}
