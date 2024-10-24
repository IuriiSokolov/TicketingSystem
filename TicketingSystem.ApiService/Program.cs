using System.Reflection;
using TicketingSystem.ApiService.DependencyInjections;
using TicketingSystem.Common.Context;
using TicketingSystem.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDbContext<TicketingDbContext>("TicketingDB");
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("Expire20", builder =>
        builder.Expire(TimeSpan.FromHours(1)));
});
builder.AddRedisOutputCache("cache");

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRedisCacheService();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddRepositories();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
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