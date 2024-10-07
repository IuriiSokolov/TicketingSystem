using TicketingSystem.ApiService.Extensions;
using TicketingSystem.Common.Context;

var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDbContext<TicketingDbContext>("TicketingDB");

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.Run();
