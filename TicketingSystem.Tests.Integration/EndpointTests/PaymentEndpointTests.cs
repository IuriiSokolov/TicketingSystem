using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.Integration.EndpointTests
{
    public class PaymentEndpointTests : IClassFixture<TicketingWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly TicketingDbContext _context;

        public PaymentEndpointTests(TicketingWebApplicationFactory applicationFactory)
        {
            _client = applicationFactory.CreateClient();
            var scope = applicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
        }

        [Fact]
        public async Task CompletePaymentTest()
        {
            // Arrange
            const int ids = 4;
            var payment = await Init(ids);

            // Act
            var result = await _client.PostAsync($"api/payments/{payment.PaymentId}/complete", null);

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task FailPaymentTest()
        {
            // Arrange
            const int ids = 5;
            var payment = await Init(ids);

            // Act
            var result = await _client.PostAsync($"api/payments/{payment.PaymentId}/failed", null);

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<Payment> Init(int ids)
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
            var cartId = Guid.NewGuid();
            var cart = new Cart
            {
                CartId = cartId,
                PersonId = ids,
                CartStatus = Common.Model.Database.Enums.CartStatus.NotPaid,
                PaymentId = ids,
            };

            var ticket = new Ticket
            {
                EventId = ids,
                PriceCategoryId = ids,
                SeatId = ids,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
                CartId = cartId
            };

            var payment = new Payment
            {
                CartId = cartId,
                PaymentId = ids,
                PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Pending,
            };

            await _context.Persons.AddAsync(person);
            await _context.PriceCategories.AddAsync(priceCategory);
            await _context.Events.AddAsync(newEvent);
            await _context.Venues.AddAsync(venue);
            await _context.Sections.AddAsync(section);
            await _context.Seats.AddAsync(seat);
            await _context.Carts.AddAsync(cart);
            await _context.Tickets.AddAsync(ticket);
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
