using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PaymentRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TicketingDbContext _context;

        public PaymentRepository(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return false;
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompletePayment(int paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.Cart)
                .ThenInclude(c => c!.Tickets)
                .FirstOrDefaultAsync(x => x.PaymentId == paymentId);
            if (payment == null
                || payment.PaymentStatus != Common.Model.Database.Enums.PaymentStatus.Pending)
                return false;
            payment.PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Paid;
            payment.PaymentTime = DateTime.UtcNow;
            payment.Cart!.CartStatus = Common.Model.Database.Enums.CartStatus.Paid;
            foreach (var ticket in payment.Cart.Tickets)
            {
                ticket.Status = Common.Model.Database.Enums.TicketStatus.Purchased;
                ticket.PersonId = payment.Cart.PersonId;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FailPayment(int paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.Cart)
                .ThenInclude(c => c!.Tickets)
                .FirstOrDefaultAsync(x => x.PaymentId == paymentId);

            if (payment == null
                || payment.PaymentStatus != Common.Model.Database.Enums.PaymentStatus.Pending)
                return false;

            payment.PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Failed;
            foreach (var ticket in payment.Cart!.Tickets)
            {
                ticket.Status = Common.Model.Database.Enums.TicketStatus.Free;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
