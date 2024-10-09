using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class EventsEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.EventId);
            builder.HasMany(x => x.Tickets)
                .WithOne(x => x.Event)
                .HasForeignKey(x => x.EventId)
                .HasPrincipalKey(x => x.EventId);
        }
    }
}
