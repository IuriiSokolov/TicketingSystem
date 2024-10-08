
namespace TicketingSystem.Common.Model.Database.Entities
{
    public class PriceCategory
    {
        public int PriceCategoryId { get; set; }
        public required string PriceCategoryName { get; set; }
        public string? PriceCategoryDescription { get; set; }
        public required float PriceUsd { get; set; }

        public required int EventId { get; set; }
        public Event? Event { get; set; }

        public ICollection<Seat> Seats { get; } = [];
    }
}
