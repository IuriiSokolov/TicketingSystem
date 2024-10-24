using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class EventRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new EventRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
