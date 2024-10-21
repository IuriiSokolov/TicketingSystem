using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.DTOs.Output
{
    public record struct TicketDto(int TicketId, int PriceCategoryId, int EventId, int SeatId, TicketStatus Status, int? PersonId, Guid? CartId)
    {
        public TicketDto(Ticket ticket) : this(ticket.TicketId, ticket.PriceCategoryId, ticket.EventId, ticket.SeatId, ticket.Status, ticket.PersonId, ticket.CartId) { }
    }
}
