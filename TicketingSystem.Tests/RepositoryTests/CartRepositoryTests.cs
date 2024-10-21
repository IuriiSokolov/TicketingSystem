using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddAsyncTest()
        {
            // Arrange
            var cart = new Cart
            {
                PersonId = 1
            };

            var mockSet = new Mock<DbSet<Cart>>();
            _mockContext.Setup(x => x.Carts).Returns(mockSet.Object);

            // Act
            var result = await _cartRepository.AddAsync(cart);

            // Assert
            result.Should().Be(cart);
            mockSet.Verify(x => x.AddAsync(cart, CancellationToken.None), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            Guid id = Guid.Empty;
            var cart = new Cart
            {
                CartId = id,
                PersonId = 1
            };

            var carts = new List<Cart>
            {
                cart
            };

            var mockSet = new Mock<DbSet<Cart>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult((Cart?)cart));
            _mockContext.Setup(x => x.Carts).Returns(mockSet.Object);

            // Act
            var result = await _cartRepository.GetByIdAsync(id);

            // Assert
            result.Should().Be(cart);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var cart = new Cart
            {
                PersonId = 1
            };

            var carts = new List<Cart>
            {
                cart
            };

            _mockContext.Setup(x => x.Carts).ReturnsDbSet(carts);

            // Act
            var result = await _cartRepository.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(carts);
            _mockContext.Verify(x => x.Carts, Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            // Arrange
            var cartOld = new Cart()
            {
                PersonId = 1
            };

            var cartNew = new Cart()
            {
                PersonId = 2
            };

            var carts = new List<Cart>
            {
                cartOld
            };

            _mockContext.Setup(x => x.Carts).ReturnsDbSet(carts);

            // Act
            var result = await _cartRepository.UpdateAsync(cartNew);

            // Assert
            result.Should().BeEquivalentTo(cartNew);
            _mockContext.Verify(x => x.Carts, Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task DeleteAsyncTest(bool cartExists, bool expectedResult)
        {
            // Arrange
            Guid id = Guid.Empty;
            Cart? cart = null;
            var carts = new List<Cart>();
            if (cartExists)
            {
                cart = new Cart()
                {
                    CartId = id,
                    PersonId = 1,
                };
                carts.Add(cart);
            }

            var mockSet = new Mock<DbSet<Cart>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult(cart));
            _mockContext.Setup(x => x.Carts).Returns(mockSet.Object);

            // Act
            var result = await _cartRepository.DeleteAsync(id);

            // Assert
            result.Should().Be(expectedResult);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
            if (cartExists)
            {
                mockSet.Verify(x => x.Remove(cart!), Times.Once);
                _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            }
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
            var result = await _cartRepository.FirstOrDefaultWithTicketsAsync(x => true);

            // Assert
            result.Should().Be(cart);
            _mockContext.Verify(x => x.Carts, Times.Once);
        }
    }
}
