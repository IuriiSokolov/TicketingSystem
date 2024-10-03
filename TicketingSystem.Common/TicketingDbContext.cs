using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using TicketingSystem.Common.Model;

namespace TicketingSystem.Common
{
    public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
    {
        public static TicketingDbContext Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuilder<TicketingDbContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Event>()
        //        .ToCollection("events");
        //}

        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
