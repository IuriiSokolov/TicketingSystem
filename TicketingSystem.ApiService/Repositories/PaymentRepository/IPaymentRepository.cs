using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PaymentRepository
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<Payment?> FirstOrDefaultWithCartWithTicketsAsync(Expression<Func<Payment, bool>> predicate);
    }
}