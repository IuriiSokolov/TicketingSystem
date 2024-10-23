using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs.Input;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.Tests.Integration
{
    public class TicketingControllerTests : IClassFixture<TicketingWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly TicketingDbContext _context;

        public TicketingControllerTests(TicketingWebApplicationFactory applicationFactory)
        {
            _client = applicationFactory.CreateClient();
            var scope = applicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
        }

        [Fact]
        public async Task GetVenuesTest()
        {
            var result = await _client.GetAsync("api/venues");
            result.Should().NotBeNull();
            var content = await result.Content.ReadFromJsonAsync<List<VenueDto>>();
        }

        [Fact]
        public async Task AddTicketToCartTest()
        {
            // Arrange
            var (cart, ticket) = await InitCartAndTicket();
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

        private async Task<(Cart, Ticket)> InitCartAndTicket()
        {
            var cartId = Guid.NewGuid();
            var cart = new Cart
            {
                CartId = cartId,
                PersonId = 1,
                CartStatus = Common.Model.Database.Enums.CartStatus.NotPaid,
            };
            await _context.Carts.AddAsync(cart);

            var ticket = new Ticket 
            { 
                EventId = 1,
                PriceCategoryId = 1,
                SeatId = 1,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
            };
            await _context.Tickets.AddAsync(ticket);

            await _context.SaveChangesAsync();
            return (cart, ticket);
        }
    }
}