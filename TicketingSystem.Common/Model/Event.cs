namespace TicketingSystem.Common.Model
{
    public class Event
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime Date { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
    }
}
