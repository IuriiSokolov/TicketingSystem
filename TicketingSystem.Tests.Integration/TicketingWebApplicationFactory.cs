using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TicketingSystem.Common.Context;
using TicketingSystem.MigrationService;

namespace TicketingSystem.Tests.Integration
{
    public sealed class TicketingWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("TicketingDB")
                .WithUsername("postgres")
                .WithPassword("1234567890")
                .WithPortBinding(5555, 5432)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configure =>
            {
                //configure.Add(new PostgresConfigSource());
            });

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TicketingDbContext>));

                if (descriptor is not null)
                    services.Remove(descriptor);

                services.AddDbContext<TicketingDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString(), sqlOptions => 
                    {
                        sqlOptions.MigrationsAssembly("TicketingSystem.MigrationService");
                        sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
                    });
                }, ServiceLifetime.Singleton);
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }

    public class PostgresConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            new PostgresConfigProvider();
    }

    public class PostgresConfigProvider : ConfigurationProvider
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public override void Load()
        {
            TaskFactory.StartNew(LoadAsync)
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public async Task LoadAsync()
        {
            var postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("TicketingDB")
                .WithUsername("postgres")
                .WithPassword("1234567890")
                .WithPortBinding(5555, 5432)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                .Build();
            await postgresContainer.StartAsync().ConfigureAwait(false);
            Set("ConnectionStrings:TicketingDB", postgresContainer.GetConnectionString());
        }
    }
}
