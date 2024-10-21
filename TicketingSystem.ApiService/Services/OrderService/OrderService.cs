using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.PriceCategoryRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPriceCategoryRepository _priceCategoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRepository _paymentRepository;

        public OrderService(ICartRepository cartRepository,
            IPriceCategoryRepository priceCategoryRepository,
            ITicketRepository ticketRepository,
            IPaymentRepository paymentRepository)
        {
            _cartRepository = cartRepository;
            _priceCategoryRepository = priceCategoryRepository;
            _ticketRepository = ticketRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<List<TicketDto>> GetTicketsInCartAsync(Guid cartId)
        {
            var tickets = await _ticketRepository.GetTicketsInCartAsync(cartId);
            var dtos = tickets.Select(ticket => new TicketDto(ticket)).ToList();
            return dtos;
        }

        public async Task<(CartDto?, string? ErrorMsg)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId)
        {
            var cart = await _cartRepository.FirstOrDefaultWithTicketsAsync(cart => cart.CartId == cartId);
            if (cart == null || cart.CartStatus == CartStatus.Paid)
                return (null, "Cart not found");
            var ticket = await _ticketRepository.FirstOrDefaultAsync(ticket => ticket.EventId == eventId
                && ticket.SeatId == seatId
                && ticket.Status == TicketStatus.Free);
            if (ticket == null)
                return (null, "Ticket not found");
            ticket.CartId = cartId;
            await _ticketRepository.UpdateAsync(ticket);

            var categories = await _priceCategoryRepository.GetWhereAsync(pc => pc.EventId == eventId);
            var totalPriceUsd = cart.Tickets.Sum(ticket => categories.Single(pc => pc.PriceCategoryId == ticket.PriceCategoryId).PriceUsd);
            var dto = new CartDto(cart, totalPriceUsd);
            return (dto, null);
        }

        public async Task<string?> RemoveTicketFromCartAsync(Guid cartId, int eventId, int seatId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null || cart.CartStatus == CartStatus.Paid)
                return "Cart not found";
            var ticket = await _ticketRepository.FirstOrDefaultAsync(ticket => ticket.CartId == cartId
                && ticket.EventId == eventId
                && ticket.SeatId == seatId
                && ticket.Status != TicketStatus.Purchased);
            if (ticket == null)
                return "Ticket not found";
            ticket.CartId = null;
            ticket.Status = TicketStatus.Free;
            await _ticketRepository.UpdateAsync(ticket);
            return null;
        }

        public async Task<PaymentDto?> BookTicketsInCart(Guid cartId)
        {
            var cart = await _cartRepository.FirstOrDefaultWithTicketsAsync(c => c.CartId == cartId);
            if (cart == null)
                return null;
            foreach (var ticket in cart.Tickets)
            {
                ticket.Status = TicketStatus.Booked;
                await _ticketRepository.UpdateAsync(ticket);
            }
            var payment = await _paymentRepository.AddAsync(new Payment { PaymentStatus = PaymentStatus.Pending, CartId = cartId, Cart = cart });
            return new PaymentDto(payment);
        }
    }
}
