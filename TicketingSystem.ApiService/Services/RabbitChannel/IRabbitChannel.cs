namespace TicketingSystem.ApiService.Services.RabbitChannel
{
    public interface IRabbitChannel
    {
        void Publish(string message);
    }
}