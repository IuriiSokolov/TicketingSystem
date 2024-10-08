using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database
{
    public class Seat
    {
        public int SeatId { get; set; }
        public required string Code { get; set; }
        public required SeatStatus Status { get; set; }

        public required int RowId { get; set; }
        //public required Row Row { get; set; }

        public required int EventId { get; set; }
        public Event? Event { get; set; }

        public Ticket Ticket { get; set; } = null!;

        //public AdmissionType IsGeneralAdmission { get; set; } //TODO change to enum
    }
}
