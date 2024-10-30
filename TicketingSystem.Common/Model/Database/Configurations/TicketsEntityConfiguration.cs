using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;
namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class TicketsEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(x => x.TicketId);
            builder.Property<uint>("Version")
                .IsRowVersion();
        }
    }
}
