using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.SectionRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class SectionRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly SectionRepository _sectionRepository;

        public SectionRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _sectionRepository = new SectionRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetWhereAsyncTest()
        {
            // Arrange
            var section = new Section
            {
                VenueId = 1
            };
            var sections = new List<Section> { section };

            _mockContext.Setup(x => x.Sections).ReturnsDbSet(sections);

            // Act
            var result = await _sectionRepository.GetWhereAsync(x => true);

            // Assert
            result.Should().BeEquivalentTo(sections);
            _mockContext.Verify(x => x.Sections, Times.Once);
        }
    }
}
