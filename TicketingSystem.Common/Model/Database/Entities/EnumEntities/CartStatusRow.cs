using TicketingSystem.Common.Model.Attributes;

namespace TicketingSystem.Common.Model.Database.Entities.EnumEntities
{
    public class CartStatusRow
    {
        [EnumValue]
        public required int CartStatusId { get; set; }
        [EnumName]
        public required string Status { get; set; }
    }
}
