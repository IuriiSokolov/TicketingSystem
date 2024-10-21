using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class PaymentRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly PaymentRepository _paymentRepository;

        public PaymentRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _paymentRepository = new PaymentRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task FirstOrDefaultWithCartWithTicketsAsyncTest()
        {
            // Arrange
            Guid cartId = Guid.Empty;

            var payment = new Payment
            {
                PaymentId = 1,
                PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Pending,
                CartId = cartId
            };
            var payments = new List<Payment> { payment };

            var cart = new Cart
            {
                CartId = cartId,
                PersonId = 1,
                PaymentId = 1,
            };
            var carts = new List<Cart> { cart };

            var ticket = new Ticket
            {
                EventId = 1,
                PriceCategoryId = 1,
                SeatId = 1,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
                CartId = cartId
            };
            var tickets = new List<Ticket> { ticket };

            _mockContext.Setup(x => x.Payments).ReturnsDbSet(payments);
            _mockContext.Setup(x => x.Carts).ReturnsDbSet(carts);
            _mockContext.Setup(x => x.Tickets).ReturnsDbSet(tickets);

            // Act
            var result = await _paymentRepository.FirstOrDefaultWithCartWithTicketsAsync(x => true);

            // Assert
            result.Should().Be(payment);
            _mockContext.Verify(x => x.Payments, Times.Once);
        }
    }
}
