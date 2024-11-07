using Mailjet.Client;
using TicketingSystem.NotificationService;
using TicketingSystem.NotificationService.EmailService;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddHostedService<NotificationHandler>();

builder.Services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
{
    client.SetDefaultSettings();
    var apiKey = builder.Configuration.GetValue<string>("MailJet:ApiKey");
    var secretKey = builder.Configuration.GetValue<string>("MailJet:SecretKey");
    client.UseBasicAuthentication(apiKey, secretKey);
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.AddRabbitMQClient("messaging");

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();

public interface INotificationMarker { }