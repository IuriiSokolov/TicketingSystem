// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Migrations;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MigrationApiInitializer<TicketingDbContext>>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationApiInitializer<TicketingDbContext>.ActivitySourceName));

builder.Services.AddDbContextPool<TicketingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TicketingDB"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        // Workaround for https://github.com/dotnet/aspire/issues/1023
        sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
    }));

var app = builder.Build();

app.Run();

public interface IMigrationServiceMarker { }