using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.ApiService.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITicketRepository _ticketRepository;
        public PaymentService(IPaymentRepository paymentRepository, ITicketRepository ticketRepository)
        {
            _paymentRepository = paymentRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<PaymentStatus?> GetStatusByIdAsync(int paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment is null)
                return null;
            return payment.PaymentStatus;
        }

        public async Task<bool> CompletePayment(int paymentId)
        {
            var payment = await _paymentRepository.FirstOrDefaultWithCartWithTicketsAsync(x => x.PaymentId == paymentId);
            if (payment == null
                || payment.PaymentStatus != PaymentStatus.Pending)
                return false;
            payment.PaymentStatus = PaymentStatus.Paid;
            payment.PaymentTime = DateTime.UtcNow;
            payment.Cart!.CartStatus = CartStatus.Paid;
            await _paymentRepository.UpdateAsync(payment);
            foreach (var ticket in payment.Cart.Tickets)
            {
                ticket.Status = TicketStatus.Purchased;
                ticket.PersonId = payment.Cart.PersonId;
                await _ticketRepository.UpdateAsync(ticket);
            }
            return true;
        }

        public async Task<bool> FailPayment(int paymentId)
        {
            var payment = await _paymentRepository.FirstOrDefaultWithCartWithTicketsAsync(x => x.PaymentId == paymentId);

            if (payment == null
                || payment.PaymentStatus != PaymentStatus.Pending)
                return false;

            payment.PaymentStatus = PaymentStatus.Failed;
            await _paymentRepository.UpdateAsync(payment);
            foreach (var ticket in payment.Cart!.Tickets)
            {
                ticket.Status = TicketStatus.Free;
                await _ticketRepository.UpdateAsync(ticket);
            }
            return true;
        }
    }
}
