using TicketingSystem.Common.Model.Attributes;

namespace TicketingSystem.Common.Model.Database.Entities.EnumEntities
{
    public class SeatTypeRow
    {
        [EnumValue]
        public required int SeatTypeId { get; set; }
        [EnumName]
        public required string SeatType { get; set; }
    }
}
