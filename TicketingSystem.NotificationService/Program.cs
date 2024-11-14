using Mailjet.Client;
using MassTransit;
using TicketingSystem.Common.Context;
using TicketingSystem.NotificationService.EmailService;
using TicketingSystem.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddRequestTimeouts();
builder.AddRedisOutputCache("cache");
builder.Services.AddRedisCacheService();
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddEntityFrameworkOutbox<NotificationDbContext>(config =>
    {
        config.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
        config.UsePostgres().UseBusOutbox();
    });
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddConsumers(typeof(Program).Assembly);
    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("messaging"));
        config.ConfigureEndpoints(context);
    });
});
builder.AddNpgsqlDbContext<NotificationDbContext>("NotificationDb");
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
{
    client.SetDefaultSettings();
    var apiKey = builder.Configuration.GetValue<string>("MailJet:ApiKey");
    var secretKey = builder.Configuration.GetValue<string>("MailJet:SecretKey");
    client.UseBasicAuthentication(apiKey, secretKey);
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseRequestTimeouts();
app.UseOutputCache();


app.Run();

public interface INotificationMarker { }