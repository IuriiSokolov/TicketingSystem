using DotNet.Testcontainers.Containers;
using Mailjet.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.RabbitMq;
using TicketingSystem.Tests.Integration.Helpers;

namespace TicketingSystem.Tests.Integration.WebFactories
{
    public sealed class NotificationServiceFactory : WebApplicationFactory<INotificationMarker>, IAsyncLifetime
    {
        private readonly RabbitMqContainer _rabbitMqContainer = RabbitMqInstance.Instance;

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(x =>
            {
                x.AddInMemoryCollection(
                    [
                        new ("ConnectionStrings:messaging", _rabbitMqContainer.GetConnectionString())
                    ]);
            });

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("IntegrationTests", "true");
            builder.ConfigureTestServices(services =>
            {
                ConfigureMailJet(services);
            });
        }

        public async Task InitializeAsync()
        {
            if (_rabbitMqContainer.State != TestcontainersStates.Running)
                await _rabbitMqContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            if (_rabbitMqContainer.State == TestcontainersStates.Running)
                await _rabbitMqContainer.DisposeAsync();
        }

        private void ConfigureMailJet(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IMailjetClient));
            var type = descriptor!.ImplementationType;
            if (descriptor is not null)
                services.Remove(descriptor);
            services.AddSingleton<IMailjetClient, FakeMailjetClient>(_ =>
            {
                return new FakeMailjetClient(new HttpClient());
            });
        }
    }
}
