using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Configurations;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Context
{
    public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new TickerEntityConfiguration());

            modelBuilder.ApplySeeding();
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatStatusRow> SeatStatuses { get; set; }
        public DbSet<SeatTypeRow> SeatTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PriceCategory> PriceCategories { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartStatusRow> CartStatuses { get; set; }
        public DbSet<CartTicket> CartsTickets { get; set; }
    }
}
