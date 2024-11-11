using MassTransit;
using TicketingSystem.Common.Model.DTOs.Other;

namespace TicketingSystem.ApiService.Services
{
    public class EmailPublisher
    {
        private readonly IBus _bus;

        public EmailPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish(Email email)
            => await _bus.Publish(email);
    }
}
