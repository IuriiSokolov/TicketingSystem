// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Context;
using TicketingSystem.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ApiDbInitializer>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(ApiDbInitializer.ActivitySourceName));

builder.Services.AddDbContextPool<TicketingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TicketingDB"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("TicketingSystem.MigrationService");
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));

var app = builder.Build();

app.Run();