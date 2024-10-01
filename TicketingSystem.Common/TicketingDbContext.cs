using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model;

namespace TicketingSystem.Common
{
    public class TicketingDbContext : DbContext
    {
        public TicketingDbContext()
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}
