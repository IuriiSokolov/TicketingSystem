using DotNet.Testcontainers.Containers;
using Mailjet.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Migrations;
using TicketingSystem.Tests.Integration.Helpers;

namespace TicketingSystem.Tests.Integration.WebFactories
{
    public sealed class NotificationServiceFactory : WebApplicationFactory<INotificationMarker>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("NotificationDb")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();

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
                ConfigurePostgres(services);
                ConfigureMailJet(services);
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            if (_rabbitMqContainer.State != TestcontainersStates.Running)
                await _rabbitMqContainer.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
            dbContext.Database.Migrate();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
            if (_rabbitMqContainer.State == TestcontainersStates.Running)
                await _rabbitMqContainer.DisposeAsync();
        }

        private void ConfigurePostgres(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<NotificationDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<NotificationDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString(), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(INotificationMigrationMarker).Assembly.FullName);
                    sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
                });
            }, ServiceLifetime.Singleton);
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
