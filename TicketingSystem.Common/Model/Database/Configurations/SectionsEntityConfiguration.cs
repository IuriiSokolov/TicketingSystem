using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class SectionsEntityConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(x => x.SectionId);
            builder.HasMany(x => x.Seats)
                .WithOne(x => x.Section)
                .HasForeignKey(x => x.SectionId)
                .HasPrincipalKey(x => x.SectionId);
        }
    }
}
