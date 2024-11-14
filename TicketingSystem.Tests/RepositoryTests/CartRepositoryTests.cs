using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class CartRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly CartRepository _cartRepository;

        public CartRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _cartRepository = new CartRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task FirstOrDefaultWithTicketsAsync()
        {
            // Arrange
            Guid cartId = Guid.Empty;

            var ticket = new Ticket
            {
                EventId = 1,
                PriceCategoryId = 1,
                SeatId = 1,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
                CartId = cartId
            };
            var tickets = new List<Ticket> { ticket };

            var cart = new Cart
            {
                CartId = cartId,
                PersonId = 1,
            };
            var carts = new List<Cart> { cart };

            _mockContext.Setup(x => x.Tickets).ReturnsDbSet(tickets);
            _mockContext.Setup(x => x.Carts).ReturnsDbSet(carts);

            // Act
            var result = await _cartRepository.FirstOrDefaultAsync(x => true, x => x.Tickets);

            // Assert
            result.Should().Be(cart);
            _mockContext.Verify(x => x.Carts, Times.Once);
        }
    }
}
