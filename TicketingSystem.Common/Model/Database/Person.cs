namespace TicketingSystem.Common.Model.Database
{
    public class Person
    {
        public int PersonId { get; set; }
        public required string Name { get; set; }
        public required string ContactInfo { get; set; }

        public ICollection<Ticket>? Tickets { get; }
    }
}
