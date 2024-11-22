using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests.RepositoryBaseTests
{
    [TestClass]
    public class RepositoryBaseTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly Mock<DbSet<Venue>> _mockSet;
        private readonly TestRepository _testRepository;

        public RepositoryBaseTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _testRepository = new TestRepository(_mockContext.Object);
            _mockSet = new Mock<DbSet<Venue>>();
            _mockContext.Setup(x => x.Set<Venue>()).Returns(_mockSet.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockContext.Verify();
            _mockSet.Verify();
        }

        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var venue = new Venue
            {
                Address = "testAddress",
                Name = "testName",
            };

            // Act
            var result = _testRepository.Add(venue);

            // Assert
            result.Should().Be(venue);
            _mockSet.Verify(x => x.Add(venue), Times.Once);
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

            _mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult((Venue?)venue));

            // Act
            var result = await _testRepository.GetByIdAsync(id);

            // Assert
            result.Should().Be(venue);
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

            _mockContext.Setup(x => x.Set<Venue>()).ReturnsDbSet(venues);

            // Act
            var result = await _testRepository.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(venues);
        }

        [TestMethod]
        public void UpdateTest()
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

            _mockContext.Setup(x => x.Set<Venue>()).ReturnsDbSet(venues);

            // Act
            var result = _testRepository.Update(venueNew);

            // Assert
            result.Should().BeEquivalentTo(venueNew);
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

            _mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult(venue));

            // Act
            var result = await _testRepository.DeleteAsync(id);

            // Assert
            result.Should().Be(expectedResult);
            if (venueExists)
            {
                _mockSet.Verify(x => x.Remove(venue!), Times.Once);
            }
        }

        [TestMethod]
        public async Task FirstOrDefaultAsync()
        {
            // Arrange
            var venue = new Venue
            {
                Address = "testAddress",
                Name = "testName",
                VenueId = 1
            };
            var venues = new List<Venue> { venue };

            var @event = new Event
            {
                EventId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testEventName",
                VenueId = 1
            };
            var events = new List<Event> { @event };

            _mockContext.Setup(x => x.Set<Venue>()).ReturnsDbSet(venues);
            _mockContext.Setup(x => x.Events).ReturnsDbSet(events);

            // Act
            var result = await _testRepository.FirstOrDefaultAsync(x => true, x => x.Events);

            // Assert
            result.Should().Be(venue);
        }

        [TestMethod]
        public async Task GetWhereAsync()
        {
            // Arrange
            var venue = new Venue
            {
                Address = "testAddress",
                Name = "testName",
                VenueId = 1
            };
            var venues = new List<Venue> { venue };

            var @event = new Event
            {
                EventId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testEventName",
                VenueId = 1
            };
            var events = new List<Event> { @event };

            _mockContext.Setup(x => x.Set<Venue>()).ReturnsDbSet(venues);
            _mockContext.Setup(x => x.Events).ReturnsDbSet(events);

            // Act
            var result = await _testRepository.GetWhereAsync(x => true, x => x.Events);

            // Assert
            result.Should().BeEquivalentTo(venues);
        }
    }
}
