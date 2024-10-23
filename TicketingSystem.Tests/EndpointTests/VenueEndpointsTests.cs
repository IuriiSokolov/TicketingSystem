using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Reflection;
using TicketingSystem.ApiService.Endpoints;
using TicketingSystem.ApiService.Services.VenueService;
using TicketingSystem.Common.Model.DTOs.Output;
using FluentAssertions;

namespace TicketingSystem.Tests.EndpointTests
{
    [TestClass]
    public class VenueEndpointsTests
    {
        private readonly Mock<IVenueService> _mockPaymentService;
        private readonly VenueEndpoints _endpoints;

        public VenueEndpointsTests()
        {
            _mockPaymentService = new Mock<IVenueService>();
            _endpoints = new VenueEndpoints();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockPaymentService.Verify();
        }

        [TestMethod]
        public async Task GetVenuesTest()
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetVenues", BindingFlags.NonPublic | BindingFlags.Instance);

            var venueId = 1;

            List<VenueDto> venueDtos =
            [
                new()
                {
                    Address = "testAddress",
                    Name = "testName",
                    VenueId = venueId
                }
            ];

            var expectedResult = TypedResults.Ok(venueDtos);

            _mockPaymentService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(venueDtos));

            // Act
            var result = await (Task<Ok<List<VenueDto>>>)method!.Invoke(_endpoints, [_mockPaymentService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task GetSectionsOfVenueTest(bool resultDtoExists, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetSectionsOfVenue", BindingFlags.NonPublic | BindingFlags.Instance);

            var venueId = 1;

            List<SectionDto>? sectionDtos = null;
            if (resultDtoExists)
            {
                sectionDtos =
                [
                    new()
                    {
                        SectionId = 1,
                        VenueId = venueId
                    }
                ];
            }

            Results<Ok<List<SectionDto>>, NotFound> expectedResult = expectedResultIsOk
                ? TypedResults.Ok(sectionDtos)
                : TypedResults.NotFound();

            _mockPaymentService.Setup(x => x.GetSectionsAsync(venueId)).Returns(Task.FromResult(sectionDtos));

            // Act
            var result = await (Task<Results<Ok<List<SectionDto>>, NotFound>>)method!.Invoke(_endpoints, [venueId, _mockPaymentService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
