using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class CartRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new CartRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
