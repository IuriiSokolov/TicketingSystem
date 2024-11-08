﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<Cart?> FirstOrDefaultAsync(Expression<Func<Cart, bool>> predicate, params Expression<Func<Cart, object>>[] includes)
        {
            IQueryable<Cart> queriable = Context.Carts;
            for (int i = 0; i < includes.Length; i++)
            {
                queriable = queriable.Include(includes[i]);
            }
            return await queriable.FirstOrDefaultAsync(predicate);
        }
    }
}
