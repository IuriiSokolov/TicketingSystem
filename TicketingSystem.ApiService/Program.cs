using MassTransit;
using Serilog;
using System.Reflection;
using TicketingSystem.ApiService.DependencyInjections;
using TicketingSystem.Common.Context;
using TicketingSystem.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDbContext<TicketingDbContext>("TicketingDB");
builder.AddRedisOutputCache("cache");
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRedisCacheService();
builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("messaging"));
        config.ConfigureEndpoints(context);
    });
});

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddRepositories();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseOutputCache();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEndpoints();
app.MapDefaultEndpoints();

app.Run();

public interface IApiMarker { }