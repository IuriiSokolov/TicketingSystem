using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.ApiService.Repositories.SectionRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests
{
    [TestClass]
    public class TicketRepositoryTests
    {
        private readonly Mock<TicketingDbContext> _mockContext;
        private readonly TicketRepository _ticketRepository;

        public TicketRepositoryTests()
        {
            _mockContext = new Mock<TicketingDbContext>();
            _ticketRepository = new TicketRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetWhereAsyncTest()
        {
            // Arrange
            var ticket = new Ticket
            {
                EventId = 1,
                PriceCategoryId = 1,
                SeatId = 1,
                Status = Common.Model.Database.Enums.TicketStatus.Free
            };
            var tickets = new List<Ticket> { ticket };

            _mockContext.Setup(x => x.Tickets).ReturnsDbSet(tickets);

            // Act
            var result = await _ticketRepository.GetWhereAsync(x => true);

            // Assert
            result.Should().BeEquivalentTo(tickets);
            _mockContext.Verify(x => x.Tickets, Times.Once);
        }

        [TestMethod]
        public async Task FirstOrDefaultWithTicketsAsync()
        {
            // Arrange
            Guid cartId = Guid.Empty;

            var ticket = new Ticket
            {
                EventId = 1,
                PriceCategoryId = 1,
                SeatId = 1,
                Status = Common.Model.Database.Enums.TicketStatus.Free,
                CartId = cartId
            };
            var tickets = new List<Ticket> { ticket };

            _mockContext.Setup(x => x.Tickets).ReturnsDbSet(tickets);

            // Act
            var result = await _ticketRepository.FirstOrDefaultAsync(x => true);

            // Assert
            result.Should().Be(ticket);
            _mockContext.Verify(x => x.Tickets, Times.Once);
        }
    }
}
