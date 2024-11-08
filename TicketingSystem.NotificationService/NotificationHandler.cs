using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TicketingSystem.Common.Model.DTOs.Other;
using TicketingSystem.NotificationService.EmailService;

namespace TicketingSystem.NotificationService
{
    public class NotificationHandler : BackgroundService
    {
        private readonly IConnection _rabbitConnection;
        private readonly IEmailService _emailService;
        private EventingBasicConsumer? _consumer;
        private IModel? _channel;
        const string Exchange = "NotificationExchange";
        const string RoutingKey = "NotificationRoutingKey";
        const string Queue = "NotificationQueue";

        public NotificationHandler(IConnection rabbitConnection, IEmailService emailService)
        {
            _rabbitConnection = rabbitConnection;
            _emailService = emailService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel = _rabbitConnection.CreateModel();
            _channel.ExchangeDeclare(Exchange, ExchangeType.Direct);
            _channel.QueueDeclare(Queue, false, false, false);
            _channel.QueueBind(Queue, Exchange, RoutingKey);
            _channel.BasicQos(0, 1, false);
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += ProcessMessageAsync;
            _channel.BasicConsume(Queue, false, _consumer);

            return Task.CompletedTask;
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _consumer!.Received -= ProcessMessageAsync;
            _channel?.Dispose();
        }

        private void ProcessMessageAsync(object? sender, BasicDeliverEventArgs args)
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());
            var email = JsonSerializer.Deserialize<Email>(json);
            _emailService.SendMail(email);
            _channel!.BasicAck(args.DeliveryTag, false);
        }

    }
}
