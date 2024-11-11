using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Configurations;
using TicketingSystem.Common.Model.Database.Configurations.Seeding;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;
using Event = TicketingSystem.Common.Model.Database.Entities.Event;

namespace TicketingSystem.Common.Context
{
    public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
    {
        public TicketingDbContext(): this(new DbContextOptions<TicketingDbContext>())
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();

            modelBuilder.ApplyConfiguration(new VenuesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SectionsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SeatsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SeatStatusesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SeatTypesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TicketsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PricesCategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PersonsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentStatusesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartStatusesEntityConfiguration());

            modelBuilder.ApplyEnumSeeding();
            modelBuilder.ApplySeeding();
        }

        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<TicketStatusRow> TicketStatuses { get; set; }
        public virtual DbSet<SeatTypeRow> SeatTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<PriceCategory> PriceCategories { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentStatusRow> PaymentStatuses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartStatusRow> CartStatuses { get; set; }
    }
}
