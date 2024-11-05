using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;
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
                .WithPassword("postgres")
                .Build();

        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("IntegrationTests", "true");
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

                descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IConnectionMultiplexer));
                if (descriptor is not null)
                    services.Remove(descriptor);
                services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(_redisContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            await _redisContainer.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
            dbContext.Database.Migrate();
        }
        public new async Task DisposeAsync()
        {
            await _redisContainer.DisposeAsync();
            await _dbContainer.DisposeAsync();
        }
    }
}
