using TicketingSystem.Tests.Integration.Helpers;
using TicketingSystem.Tests.Integration.WebFactories;
using Xunit.Extensions.AssemblyFixture;

[assembly: TestFramework(AssemblyFixtureFramework.TypeName, AssemblyFixtureFramework.AssemblyName)]
namespace TicketingSystem.Tests.Integration
{
    public class TestFixture : IAsyncLifetime
    {
        public ApiServiceFactory ApiServiceFactory { get; init; }
        public HttpClient ApiServiceClient { get; set; }
        public NotificationServiceFactory NotificationServiceFactory { get; init; }

        public TestFixture()
        {
            ApiServiceFactory = new ApiServiceFactory();
            NotificationServiceFactory = new NotificationServiceFactory();
        }

        public async Task InitializeAsync()
        {
            await ApiServiceFactory.InitializeAsync();
            ApiServiceClient = ApiServiceFactory.CreateClient();
            await NotificationServiceFactory.InitializeAsync();
            NotificationServiceFactory.CreateClient();
        }
        public async Task DisposeAsync()
        {
            await ApiServiceFactory.DisposeAsync();
            await NotificationServiceFactory.DisposeAsync();
        }
    }
}
