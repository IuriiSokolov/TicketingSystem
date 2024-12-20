﻿using MassTransit;
using TicketingSystem.ApiService.Cache;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.PriceCategoryRepository;
using TicketingSystem.ApiService.Repositories.SeatRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.ApiService.Repositories.UnitOfWork;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;
using TicketingSystem.Common.Model.DTOs.Other;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.Redis;

namespace TicketingSystem.ApiService.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPriceCategoryRepository _priceCategoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRedisCacheService _cache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderService> _logger;
        private readonly IBus _bus;

        public OrderService(ICartRepository cartRepository,
            IPriceCategoryRepository priceCategoryRepository,
            ITicketRepository ticketRepository,
            IPaymentRepository paymentRepository,
            IRedisCacheService cache,
            ISeatRepository seatRepository,
            IUnitOfWork unitOfWork,
            ILogger<OrderService> logger,
            IBus bus)
        {
            _cartRepository = cartRepository;
            _priceCategoryRepository = priceCategoryRepository;
            _ticketRepository = ticketRepository;
            _paymentRepository = paymentRepository;
            _cache = cache;
            _seatRepository = seatRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _bus = bus;
        }

        public async Task<List<TicketDto>> GetTicketsInCartAsync(Guid cartId)
        {
            var tickets = await _ticketRepository.GetWhereAsync(ticket => ticket.CartId == cartId);
            var dtos = tickets.Select(ticket => new TicketDto(ticket)).ToList();
            return dtos;
        }

        public async Task<(CartDto? Dto, string? ErrorMsg)> AddTicketToCartAsync(Guid cartId, int eventId, int seatId)
        {
            var (result, exceptionMsg) = await _unitOfWork.DoInTransaction<(CartDto? Dto, string? emailAddress, string? ErrorMsg)>(
                async () => await AddTicketToCartPlainAsync(cartId, eventId, seatId), System.Data.IsolationLevel.RepeatableRead);
            var errorMsg = exceptionMsg ?? result.ErrorMsg;
            if (errorMsg == null)
            {
                _logger.LogInformation("Ticket added to the cart {cartId} successfully", cartId);
            }
            else
                _logger.LogWarning("Error while adding the ticket to the cart {cartId}. Message: {errorMsg}", cartId, errorMsg);
            return (result.Dto, errorMsg);
        }

        private async Task<(CartDto? Dto, string? emailAddress, string? ErrorMsg)> AddTicketToCartPlainAsync(Guid cartId, int eventId, int seatId)
        {
            var cart = await _cartRepository.FirstOrDefaultAsync(cart => cart.CartId == cartId, x => x.Tickets, y => y.Person!);
            if (cart == null || cart.CartStatus == CartStatus.Paid)
                return (null, null, "Cart not found");
            var ticket = await _ticketRepository.FirstOrDefaultAsync(ticket => ticket.EventId == eventId
                && ticket.SeatId == seatId
                && ticket.Status == TicketStatus.Free
                && ticket.CartId == null,
                x => x.Seat!);
            if (ticket == null)
                return (null, null, "Ticket not found");
            ticket.CartId = cartId;
            _ticketRepository.Update(ticket);

            await _cache.DeleteAsync(CacheKeys.GetSeatsOfSectionOfEvent(eventId, ticket.Seat!.SectionId!.Value));

            var emailAddress = cart.Person!.Email;
            if (emailAddress is not null)
            {
                var email = new Email(emailAddress, "Ticket added to cart", $"Ticket added to the cart {cartId} successfully");
                await _bus.Publish(email);
            }
            await _unitOfWork.SaveChangesAsync();

            var categories = await _priceCategoryRepository.GetWhereAsync(pc => pc.EventId == eventId);
            var totalPriceUsd = cart.Tickets.Sum(ticket => categories.Single(pc => pc.PriceCategoryId == ticket.PriceCategoryId).PriceUsd);
            var dto = new CartDto(cart, totalPriceUsd);
            return (dto, cart.Person!.Email, null);
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
            _ticketRepository.Update(ticket);
            await _unitOfWork.SaveChangesAsync();
            return null;
        }

        public async Task<PaymentDto?> BookTicketsInCart(Guid cartId)
        {
            var cart = await _cartRepository.FirstOrDefaultAsync(c => c.CartId == cartId, x => x.Tickets);
            if (cart == null)
                return null;
            foreach (var ticket in cart.Tickets)
            {
                ticket.Status = TicketStatus.Booked;
                _ticketRepository.Update(ticket);
            }
            var payment = _paymentRepository.Add(new Payment { PaymentStatus = PaymentStatus.Pending, CartId = cartId, Cart = cart });
            await _unitOfWork.SaveChangesAsync();
            return new PaymentDto(payment);
        }
    }
}
