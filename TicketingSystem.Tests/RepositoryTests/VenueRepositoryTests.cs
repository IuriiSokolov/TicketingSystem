using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Configurations;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class VenueRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly VenueRepository _venueRepository;

        public VenueRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _venueRepository = new VenueRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            // Arrange
            var venue = new Venue
            {
                Address = "testAddress",
                Name = "testName",
            };

            var mockSet = new Mock<DbSet<Venue>>();
            _mockContext.Setup(x => x.Venues).Returns(mockSet.Object);

            // Act
            var result = await _venueRepository.AddAsync(venue);

            // Assert
            result.Should().Be(venue);
            mockSet.Verify(x => x.AddAsync(venue, CancellationToken.None), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            const int id = 1;
            var venue = new Venue()
            {
                VenueId = id,
                Address = "testAddress",
                Name = "testName",
            };

            var venues = new List<Venue>
            {
                venue
            };

            var mockSet = new Mock<DbSet<Venue>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult((Venue?)venue));
            _mockContext.Setup(x => x.Venues).Returns(mockSet.Object);

            // Act
            var result = await _venueRepository.GetByIdAsync(id);

            // Assert
            result.Should().Be(venue);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var venue = new Venue()
            {
                Address = "testAddress",
                Name = "testName",
            };

            var venues = new List<Venue>
            {
                venue
            };

            _mockContext.Setup(x => x.Venues).ReturnsDbSet(venues);

            // Act
            var result = await _venueRepository.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(venues);
            _mockContext.Verify(x => x.Venues, Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            // Arrange
            var venueOld = new Venue()
            {
                Address = "testAddressOld",
                Name = "testNameOld",
            };

            var venueNew = new Venue()
            {
                Address = "testAddress",
                Name = "testName",
            };

            var venues = new List<Venue>
            {
                venueOld
            };

            _mockContext.Setup(x => x.Venues).ReturnsDbSet(venues);

            // Act
            var result = await _venueRepository.UpdateAsync(venueNew);

            // Assert
            result.Should().BeEquivalentTo(venueNew);
            _mockContext.Verify(x => x.Venues, Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task DeleteAsyncTest(bool venueExists, bool expectedResult)
        {
            // Arrange
            const int id = 1;
            Venue? venue = null;
            var venues = new List<Venue>();
            if (venueExists)
            {
                venue = new Venue()
                {
                    VenueId = id,
                    Address = "testAddress",
                    Name = "testName",
                };
                venues.Add(venue);
            }

            var mockSet = new Mock<DbSet<Venue>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult(venue));
            _mockContext.Setup(x => x.Venues).Returns(mockSet.Object);

            // Act
            var result = await _venueRepository.DeleteAsync(id);

            // Assert
            result.Should().Be(expectedResult);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
            if (venueExists)
            {
                mockSet.Verify(x => x.Remove(venue!), Times.Once);
                _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            }
        }
    }
}
