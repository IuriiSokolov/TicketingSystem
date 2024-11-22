using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class TicketRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new TicketRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
