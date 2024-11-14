using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Common.Context
{
    public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
    {
        public NotificationDbContext() : this(new DbContextOptions<NotificationDbContext>())
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
        }
    }
}
