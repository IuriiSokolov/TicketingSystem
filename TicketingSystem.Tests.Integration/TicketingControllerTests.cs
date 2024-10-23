using FluentAssertions;
using System.Net.Http.Json;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.Tests.Integration
{
    public class TicketingControllerTests : IClassFixture<TicketingWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TicketingControllerTests(TicketingWebApplicationFactory applicationFactory)
        {
            _client = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task GetVenuesTest()
        {
            var result = await _client.GetAsync("api/venues");
            result.Should().NotBeNull();
            var content = await result.Content.ReadFromJsonAsync<List<VenueDto>>();
        }
    }
}