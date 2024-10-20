using System.Linq.Expressions;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment payment);
        Task<bool> DeleteAsync(int id);
        Task<Payment?> FirstOrDefaultWithCartWithTicketsAsync(Expression<Func<Payment, bool>> predicate);
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> UpdateAsync(Payment payment);
    }
}