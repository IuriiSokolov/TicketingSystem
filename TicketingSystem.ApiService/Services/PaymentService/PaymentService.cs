using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.ApiService.Repositories.UnitOfWork;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.ApiService.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;
        private readonly TimeSpan PaymentShelfLife;
        public PaymentService(IPaymentRepository paymentRepository, ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IConfiguration configuration, ITimeProvider timeProvider)
        {
            _paymentRepository = paymentRepository;
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            var paymentShelfLifeMin = Convert.ToInt32(configuration["PaymentShelfLifeMin"]);
            PaymentShelfLife = TimeSpan.FromMinutes(paymentShelfLifeMin);
            _timeProvider = timeProvider;
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

            FailPayment(payment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task FailOutdatedPayments()
        {
            var payments = await _paymentRepository.GetWhereWithCartWithTicketsAsync(x => _timeProvider.GetUtcNow() - x.CreationTime > PaymentShelfLife
                && x.PaymentStatus == PaymentStatus.Pending);
            foreach (var payment in payments)
            {
                FailPayment(payment);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private static void FailPayment(Payment payment)
        {
            payment.PaymentStatus = PaymentStatus.Failed;
            foreach (var ticket in payment.Cart!.Tickets)
            {
                ticket.Status = TicketStatus.Free;
            }
        }
    }
}
