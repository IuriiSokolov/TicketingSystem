﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;
using TicketingSystem.Common.Context;
using TicketingSystem.Tests.Integration.Helpers;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using TicketingSystem.Common.Migrations;

namespace TicketingSystem.Tests.Integration.WebFactories
{
    public sealed class ApiServiceFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
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
                ConfigureRedis(services);
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            await _redisContainer.StartAsync();
            if (_rabbitMqContainer.State != TestcontainersStates.Running)
                await _rabbitMqContainer.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
            dbContext.Database.Migrate();
        }
        public new async Task DisposeAsync()
        {
            await _redisContainer.DisposeAsync();
            await _dbContainer.DisposeAsync();
            if (_rabbitMqContainer.State == TestcontainersStates.Running)
                await _rabbitMqContainer.DisposeAsync();
        }

        private void ConfigurePostgres(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TicketingDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<TicketingDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString(), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(IMigrationServiceMarker).Assembly.FullName);
                    sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
                });
            }, ServiceLifetime.Singleton);
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IConnectionMultiplexer));
            if (descriptor is not null)
                services.Remove(descriptor);
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(_redisContainer.GetConnectionString()));
        }
    }
}
