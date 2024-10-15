using TicketingSystem.Common.Model.Database.Enums;
namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public required SeatType SeatType { get; set; }
        public int? RowNumber { get; set; }

        public int? SectionId { get; set; }
        public Section? Section { get; set; }

        public required int EventId { get; set; }
        public Event? Event { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
