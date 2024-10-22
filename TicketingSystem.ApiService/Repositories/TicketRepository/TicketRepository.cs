using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public class TicketRepository(TicketingDbContext context) : RepositoryBase<Ticket>(context), ITicketRepository
    {
        public async Task<Ticket?> FirstOrDefaultAsync(Expression<Func<Ticket, bool>> predicate)
        {
            return await Context.Tickets.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Ticket>> GetWhereAsync(Expression<Func<Ticket, bool>> predicate, params Expression<Func<Ticket, object>>[] includes)
        {
            IQueryable<Ticket> queriable = Context.Tickets;
            for (int i = 0; i < includes.Length; i++)
            {
                queriable = queriable.Include(includes[i]);
            }
            return await queriable.Where(predicate).ToListAsync();
        }
    }
}
