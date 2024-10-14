using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.DTOs
{
    public class TicketsFromEventAndSectionDto
    {
        public required int SectionId { get; set; }
        public int? RowNumber { get; set; }
        public required int SeatId { get; set; }
        public required TicketStatus SeatStatus { get; set; }
        public required int PriceCategoryId { get; set; }
        public required string PriceCategoryName{ get; set; }
    }
}
