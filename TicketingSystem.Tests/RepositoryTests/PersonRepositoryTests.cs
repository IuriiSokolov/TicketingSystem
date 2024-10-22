using FluentAssertions;
using Moq;
using TicketingSystem.ApiService.Repositories.PersonRepository;
using TicketingSystem.Common.Context;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            var mockContext = new Mock<TicketingDbContext>();

            // Act
            var repository = new PersonRepository(mockContext.Object);

            // Assert
            repository.Should().NotBeNull();
        }
    }
}
