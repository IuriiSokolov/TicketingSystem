using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using TicketingSystem.ApiService.Endpoints;
using TicketingSystem.ApiService.Services.EventService;
using TicketingSystem.Common.Model.DTOs.Output;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TicketingSystem.Tests.EndpointTests
{
    [TestClass]
    public class EventEndpointsTests
    {
        private readonly Mock<IEventService> _mockEventService;
        private readonly EventEndpoints _endpoints;

        public EventEndpointsTests()
        {
            _mockEventService = new Mock<IEventService>();
            _endpoints = new EventEndpoints();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockEventService.Verify();
        }

        [TestMethod]
        public async Task GetSeatsOfSectionOfEventTest()
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetSeatsOfSectionOfEvent", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventId = 1;
            var sectionId = 1;

            var ticketDtos = new List<TicketsFromEventAndSectionDto> 
            {
                new() 
                {
                    PriceCategoryId = 1,
                    PriceCategoryName = "testPCName",
                    SeatId = 1,
                    SeatStatus = Common.Model.Database.Enums.TicketStatus.Free,
                    SectionId = sectionId,
                }
            };

            var expectedResult = TypedResults.Ok(ticketDtos);

            _mockEventService.Setup(x => x.GetTicketsOfSectionOfEventAsync(eventId, sectionId)).Returns(Task.FromResult(ticketDtos));

            // Act
            var result = await (Task<Ok<List<TicketsFromEventAndSectionDto>>>)method!.Invoke(_endpoints, [eventId, sectionId, _mockEventService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public async Task GetEvents()
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetEvents", BindingFlags.NonPublic | BindingFlags.Instance);

            var eventDtos = new List<EventDto>
            {
                new() 
                {
                    Date = new DateTime(2024, 1, 1),
                    EventId = 1,
                    Name = "testName",
                }
            };

            var expectedResult = TypedResults.Ok(eventDtos);

            _mockEventService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(eventDtos));

            // Act
            var result = await (Task<Ok<List<EventDto>>>)method!.Invoke(_endpoints, [_mockEventService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
