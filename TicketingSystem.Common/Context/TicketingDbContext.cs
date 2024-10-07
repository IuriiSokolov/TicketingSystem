using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using TicketingSystem.Common.Model.Database;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Context
{
    public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options), ITicketingDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Payment)
                .WithOne(e => e.Ticket)
                .HasForeignKey<Payment>();

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Seat)
                .WithOne(e => e.Ticket)
                .HasForeignKey<Seat>();

            var initialVenue = new Venue { VenueId = 1, Address = "ул. Зарафшан, 28", Name = "Большой театр Навои" };
            var initialEvent = new Event { EventId = 1, Date = new DateTime(2024, 12, 31).ToUniversalTime(), Name = "Новогодний спектакль", VenueId = 1 };
            var initialSeat = new Seat { SeatId = 1, Code = "1", Status = SeatStatus.Purchased, EventId = 1 };
            var initialPerson = new Person { PersonId = 1, Name = "Юрий", ContactInfo = "testContact" };
            var initialPayment = new Payment { PaymentId = 1, PaymentTime = new DateTime(2024, 12, 1).ToUniversalTime() };
            var initialTicket = new Ticket { TicketId = 1, PersonId = 1, PriceUsd = 10 };

            modelBuilder.Entity<Venue>().HasData(initialVenue);
            modelBuilder.Entity<Event>().HasData(initialEvent);
            modelBuilder.Entity<Seat>().HasData(initialSeat);
            modelBuilder.Entity<Person>().HasData(initialPerson);
            modelBuilder.Entity<Payment>().HasData(initialPayment);
            modelBuilder.Entity<Ticket>().HasData(initialTicket);

            var seatStatusValues = Enum.GetValues<SeatStatus>().Select(x => (int)x).ToList();
            for (int i = seatStatusValues.Min(); i <= seatStatusValues.Max(); i++)
            {
                var seatStatus = new SeatStatusRow
                {
                    SeatStatusId = i,
                    Status = Enum.GetName((SeatStatus)i)!
                };
                modelBuilder.Entity<SeatStatusRow>().HasData(seatStatus);
            }
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SeatStatusRow> SeatStatuses { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
