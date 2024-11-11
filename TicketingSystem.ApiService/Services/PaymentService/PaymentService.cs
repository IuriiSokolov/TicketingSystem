using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.ApiService.Repositories.UnitOfWork;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.ApiService.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IPaymentRepository paymentRepository, ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
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
            _paymentRepository.Update(payment);
            foreach (var ticket in payment.Cart.Tickets)
            {
                ticket.Status = TicketStatus.Purchased;
                ticket.PersonId = payment.Cart.PersonId;
                _ticketRepository.Update(ticket);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FailPayment(int paymentId)
        {
            var payment = await _paymentRepository.FirstOrDefaultWithCartWithTicketsAsync(x => x.PaymentId == paymentId);

            if (payment == null
                || payment.PaymentStatus != PaymentStatus.Pending)
                return false;

            payment.PaymentStatus = PaymentStatus.Failed;
            _paymentRepository.Update(payment);
            foreach (var ticket in payment.Cart!.Tickets)
            {
                ticket.Status = TicketStatus.Free;
                _ticketRepository.Update(ticket);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
