using System.Reflection;
using TicketingSystem.ApiService.DependencyInjections;
using TicketingSystem.Common.Context;

var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDbContext<TicketingDbContext>("TicketingDB");

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddRepositories();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEndpoints();
app.MapDefaultEndpoints();

app.Run();