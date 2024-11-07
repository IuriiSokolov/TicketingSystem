using RabbitMQ.Client;
using System.Text;

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

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(Exchange, RoutingKey, null, body);
        }
    }
}
