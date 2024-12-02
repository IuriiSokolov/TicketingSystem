using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.PriceCategoryRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class PriceCategoryRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new PriceCategoryRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
