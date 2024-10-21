using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class EventRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly EventRepository _eventRepository;

        public EventRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _eventRepository = new EventRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            // Arrange
            var @event = new Event
            {
                VenueId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testName",
            };

            var mockSet = new Mock<DbSet<Event>>();
            _mockContext.Setup(x => x.Events).Returns(mockSet.Object);

            // Act
            var result = await _eventRepository.AddAsync(@event);

            // Assert
            result.Should().Be(@event);
            mockSet.Verify(x => x.AddAsync(@event, CancellationToken.None), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            const int id = 1;
            var @event = new Event()
            {
                EventId = id,
                VenueId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testName",
            };

            var @events = new List<Event>
            {
                @event
            };

            var mockSet = new Mock<DbSet<Event>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult((Event?)@event));
            _mockContext.Setup(x => x.Events).Returns(mockSet.Object);

            // Act
            var result = await _eventRepository.GetByIdAsync(id);

            // Assert
            result.Should().Be(@event);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var @event = new Event()
            {
                VenueId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testName",
            };

            var @events = new List<Event>
            {
                @event
            };

            _mockContext.Setup(x => x.Events).ReturnsDbSet(@events);

            // Act
            var result = await _eventRepository.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(@events);
            _mockContext.Verify(x => x.Events, Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            // Arrange
            var @eventOld = new Event()
            {
                VenueId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testNameOld",
            };

            var @eventNew = new Event()
            {
                VenueId = 1,
                Date = new DateTime(2024, 1, 1),
                Name = "testName",
            };

            var @events = new List<Event>
            {
                @eventOld
            };

            _mockContext.Setup(x => x.Events).ReturnsDbSet(@events);

            // Act
            var result = await _eventRepository.UpdateAsync(@eventNew);

            // Assert
            result.Should().BeEquivalentTo(@eventNew);
            _mockContext.Verify(x => x.Events, Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task DeleteAsyncTest(bool eventExists, bool expectedResult)
        {
            // Arrange
            const int id = 1;
            Event? @event = null;
            var events = new List<Event>();
            if (eventExists)
            {
                @event = new Event()
                {
                    EventId = id,
                    VenueId = 1,
                    Date = new DateTime(2024, 1, 1),
                    Name = "testName",
                };
                events.Add(@event);
            }

            var mockSet = new Mock<DbSet<Event>>();
            mockSet.Setup(x => x.FindAsync(id)).Returns(ValueTask.FromResult(@event));
            _mockContext.Setup(x => x.Events).Returns(mockSet.Object);

            // Act
            var result = await _eventRepository.DeleteAsync(id);

            // Assert
            result.Should().Be(expectedResult);
            mockSet.Verify(x => x.FindAsync(id), Times.Once);
            if (eventExists)
            {
                mockSet.Verify(x => x.Remove(@event!), Times.Once);
                _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            }
        }
    }
}
