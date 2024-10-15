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

        public async Task<(Cart?, float TotalPriceUsd)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.EventId == eventId 
                && ticket.SeatId == seatId
                && ticket.Status == Common.Model.Database.Enums.TicketStatus.Free);
            if (ticket == null)
                return (null, 0);
            ticket.CartId = cartId;
            await _context.SaveChangesAsync();
            var cart = await _context.Carts
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(cart => cart.CartId == cartId);
            var categories = await _context.PriceCategories.Where(pc => pc.EventId == eventId).ToListAsync();
            var totalPriceUsd = cart!.Tickets.Sum(ticket => categories.Single(pc => pc.PriceCategoryId == ticket.PriceCategoryId).PriceUsd);
            return (cart, totalPriceUsd);
        }

        public async Task<bool> RemoveTicketFromCartAsync(Guid cartId, int eventId, int seatId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.CartId == cartId
                && ticket.EventId == eventId
                && ticket.SeatId == seatId);
            if (ticket == null)
                return false;
            ticket.CartId = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Payment?> BookTicketsInCart(Guid cartId)
        {
            var cart = await _context.Carts.Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
            if (cart == null)
                return null;
            foreach (var ticket in cart.Tickets)
            {
                ticket.Status = Common.Model.Database.Enums.TicketStatus.Booked;
            }
            var payment = _context.Payments.Add(new Payment { PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Pending, Cart = cart });
            await _context.SaveChangesAsync();
            return payment.Entity;
        }
    }
}
