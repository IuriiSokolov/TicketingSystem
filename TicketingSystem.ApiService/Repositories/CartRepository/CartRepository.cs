using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.CartRepository
{
    public class CartRepository(TicketingDbContext context) : RepositoryBase<Cart>(context), ICartRepository
    {
        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await Context.Carts.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var cart = await Context.Carts.FindAsync(id);
            if (cart == null)
            {
                return false;
            }

            Context.Carts.Remove(cart);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
