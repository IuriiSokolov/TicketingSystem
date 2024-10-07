using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database
{
    public class Seat
    {
        public int SeatId { get; set; }
        public required string Code { get; set; }
        public required SeatStatus Status { get; set; }

        public required int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
