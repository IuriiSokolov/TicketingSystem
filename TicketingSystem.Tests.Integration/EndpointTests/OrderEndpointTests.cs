using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Input;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.Tests.Integration.EndpointTests
{
    public class OrderEndpointTests : IClassFixture<TicketingWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly TicketingDbContext _context;

        public OrderEndpointTests(TicketingWebApplicationFactory applicationFactory)
        {
            _client = applicationFactory.CreateClient();
            var scope = applicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
        }

        [Fact]
        public async Task AddTicketToCartTest()
        {
            // Arrange
            const int ids = 1;
            await Init(ids);
            var (cart, ticket) = await InitCartAndTicket(ids);
            var inputDto = new AddTicketToCartDto
            {
                EventId = ticket.EventId,
                SeatId = ticket.SeatId,
            };

            var expectedTicketDto = new TicketDto(ticket)
            {
                CartId = cart.CartId,
            };
            var expectedCartDto = new CartDto(cart)
            {
                TotalPriceUsd = 10,
                Tickets = [expectedTicketDto]
            };

            // Act
            var result = await _client.PostAsJsonAsync($"api/orders/carts/{cart.CartId}", inputDto);

            // Assert
            var resultCartDto = await result.Content.ReadFromJsonAsync<CartDto>();
            resultCartDto.Should().BeEquivalentTo(expectedCartDto);
        }

        [Fact]
        public async Task RemoveTicketFromCartTest()
        {
            // Arrange
            const int ids = 2;
            await Init(ids);
            var (cart, ticket) = await InitCartAndTicket(ids, true);

            // Act
            var result = await _client.DeleteAsync($"api/orders/carts/{cart.CartId}/events/{ticket.EventId}/seats/{ticket.SeatId}");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task BookTicketsInCartTest()
        {
            // Arrange
            const int ids = 3;
            await Init(ids);
            var (cart, _) = await InitCartAndTicket(ids, true);

            var expectedPaymentDto = new PaymentDto
            {
                PaymentId = 1,
                PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Pending,
                PaymentTime = null
            };

            // Act
            var result = await _client.PutAsync($"api/orders/carts/{cart.CartId}/book", null);

            // Assert
            var resultPaymentDto = await result.Content.ReadFromJsonAsync<PaymentDto>();
            resultPaymentDto.Should().BeEquivalentTo(expectedPaymentDto);
        }

        private async Task<(Cart, Ticket)> InitCartAndTicket(int ids, bool ticketInCart = false)
        {
            var cartId = Guid.NewGuid();
            var cart = new Cart
            {
                CartId = cartId,
                PersonId = ids,
                CartStatus = Common.Model.Database.Enums.CartStatus.NotPaid,
            };
            await _context.Carts.AddAsync(cart);

            var ticket = new Ticket
            {
                EventId = ids,
                PriceCategoryId = ids,
                SeatId = ids,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
                CartId = ticketInCart ? cartId : null
            };
            await _context.Tickets.AddAsync(ticket);

            await _context.SaveChangesAsync();
            return (cart, ticket);
        }

        private async Task Init(int ids)
        {
            var person = new Person
            {
                PersonId = ids,
                Name = "TestPerson",
                ContactInfo = ""
            };
            var priceCategory = new PriceCategory
            {
                EventId = ids,
                PriceCategoryId = ids,
                PriceCategoryName = "testPC",
                PriceUsd = 10
            };
            var newEvent = new Event
            {
                EventId = ids,
                Date = new DateTime(2024, 1, 1).ToUniversalTime(),
                Name = "testEvent",
                VenueId = ids
            };
            var venue = new Venue
            {
                VenueId = ids,
                Address = "testAddress",
                Name = "testEvent"
            };
            var section = new Section
            {
                SectionId = ids,
                VenueId = ids
            };
            var seat = new Seat
            {
                SeatId = ids,
                EventId = ids,
                SeatType = Common.Model.Database.Enums.SeatType.DesignatedSeat,
                RowNumber = 1,
                SectionId = ids,
            };
            await _context.Persons.AddAsync(person);
            await _context.PriceCategories.AddAsync(priceCategory);
            await _context.Events.AddAsync(newEvent);
            await _context.Venues.AddAsync(venue);
            await _context.Sections.AddAsync(section);
            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();
        }
    }
}