using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class TickerEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(e => e.Seat)
                .WithOne(e => e.Ticket)
                .HasForeignKey<Seat>();
        }
    }
}
