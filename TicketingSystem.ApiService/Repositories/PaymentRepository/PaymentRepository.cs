using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PaymentRepository
{
    public class PaymentRepository(TicketingDbContext context) : RepositoryBase<Payment>(context), IPaymentRepository
    {
        public async Task<Payment?> FirstOrDefaultWithCartWithTicketsAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await Context.Payments
                .Include(p => p.Cart)
                .ThenInclude(c => c!.Tickets)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
