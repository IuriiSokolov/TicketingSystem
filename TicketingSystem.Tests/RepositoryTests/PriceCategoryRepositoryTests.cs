using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.PriceCategoryRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class PriceCategoryRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly PriceCategoryRepository _priceCategoryRepository;

        public PriceCategoryRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _priceCategoryRepository = new PriceCategoryRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetWhereAsync()
        {
            // Arrange
            Guid cartId = Guid.Empty;

            var priceCategory = new PriceCategory
            {
                EventId = 1,
                PriceCategoryName = "testName",
                PriceUsd = 1
            };
            var priceCategories = new List<PriceCategory> { priceCategory };

            _mockContext.Setup(x => x.PriceCategories).ReturnsDbSet(priceCategories);

            // Act
            var result = await _priceCategoryRepository.GetWhereAsync(x => true);

            // Assert
            result.Should().BeEquivalentTo(priceCategories);
            _mockContext.Verify(x => x.PriceCategories, Times.Once);
        }
    }
}
