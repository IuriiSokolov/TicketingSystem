using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TicketingSystem.Common.Model.DTOs.Other;

namespace TicketingSystem.ApiService.Services.RabbitChannel
{
    public class RabbitChannel : IRabbitChannel
    {
        private readonly IConnection _rabbitConnection;
        private readonly IModel? _channel;
        const string Exchange = "NotificationExchange";
        const string RoutingKey = "NotificationRoutingKey";
        const string Queue = "NotificationQueue";

        public RabbitChannel(IConnection rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
            _channel = _rabbitConnection.CreateModel();
            _channel.ExchangeDeclare(Exchange, ExchangeType.Direct);
            _channel.QueueDeclare(Queue, false, false, false);
            _channel.QueueBind(Queue, Exchange, RoutingKey);
            _channel.BasicQos(0, 1, false);
        }

        public void Publish(string? emailAddress, string subject, string message)
        {
            if (emailAddress is null)
                return;
            var email = new Email(emailAddress, subject, message);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(email));
            _channel.BasicPublish(Exchange, RoutingKey, null, body);
        }
    }
}
