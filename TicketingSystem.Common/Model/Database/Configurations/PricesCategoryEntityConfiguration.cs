using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class PricesCategoryEntityConfiguration : IEntityTypeConfiguration<PriceCategory>
    {
        public void Configure(EntityTypeBuilder<PriceCategory> builder)
        {
            builder.HasKey(x => x.PriceCategoryId);
            builder.HasMany(x => x.Seats)
                .WithOne(x => x.PriceCategory)
                .HasForeignKey(x => x.PriceCategoryId)
                .HasPrincipalKey(x => x.PriceCategoryId);
        }
    }
}
