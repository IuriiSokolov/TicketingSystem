using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Common
{
    public class TicketingDbContext : DbContext
    {
        public TicketingDbContext(DbContextOptions<TicketingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }


    }
}
