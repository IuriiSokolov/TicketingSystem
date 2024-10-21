using TicketingSystem.Common.Model.Attributes;

namespace TicketingSystem.Common.Model.Database.Entities.EnumEntities
{
    public class PaymentStatusRow
    {
        [EnumValue]
        public required int PaymentStatusId { get; set; }
        [EnumName]
        public required string Status { get; set; }
    }
}
