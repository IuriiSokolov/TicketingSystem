using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.SectionRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class SectionRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new SectionRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
