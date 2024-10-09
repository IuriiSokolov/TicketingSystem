using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class VenuesEntityConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasKey(x => x.VenueId);
            builder.HasMany(x => x.Sections)
                .WithOne(x => x.Venue)
                .HasForeignKey(x => x.VenueId)
                .HasPrincipalKey(x => x.VenueId);
            builder.HasMany(x => x.Events)
                .WithOne(x => x.Venue)
                .HasForeignKey(x => x.VenueId)
                .HasPrincipalKey(x => x.VenueId);
        }
    }
}
