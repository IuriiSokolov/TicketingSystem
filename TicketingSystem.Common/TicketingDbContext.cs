using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model;

namespace TicketingSystem.Common
{
    public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
