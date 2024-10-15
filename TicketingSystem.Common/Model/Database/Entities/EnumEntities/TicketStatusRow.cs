using TicketingSystem.Common.Model.Attributes;

namespace TicketingSystem.Common.Model.Database.Entities.EnumEntities
{
    public class TicketStatusRow
    {
        [EnumValue]
        public required int TicketStatusId { get; set; }
        [EnumName]
        public required string Status { get; set; }
    }
}
