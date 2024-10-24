using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.SeatRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class SeatRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new SeatRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
