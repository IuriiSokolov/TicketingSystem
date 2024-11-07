using Testcontainers.RabbitMq;

namespace TicketingSystem.Tests.Integration.Helpers
{
    public sealed class RabbitMqInstance
    {
        private static readonly Lazy<RabbitMqContainer> lazy = 
            new(() => new RabbitMqBuilder()
            .WithImage("rabbitmq:latest")
            .Build());

        public static RabbitMqContainer Instance { get { return lazy.Value; } }
    }
}
