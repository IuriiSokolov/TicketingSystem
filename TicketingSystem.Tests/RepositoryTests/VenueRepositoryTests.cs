using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class VenueRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new VenueRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
