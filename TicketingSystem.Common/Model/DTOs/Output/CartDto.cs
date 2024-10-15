using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.DTOs.Output
{
    public record struct CartDto(Guid CartId, CartStatus CartStatus, int PersonId, int? PaymentId, List<TicketDto> Tickets, float? TotalPriceUsd)
    {
        public CartDto(Cart cart, float? totalPriceUsd = null) : this(cart.CartId, cart.CartStatus, cart.PersonId, cart.PaymentId, [], totalPriceUsd)
        {
            Tickets = cart.Tickets.Select(ticket => new TicketDto(ticket)).ToList();
        }
    }
}
