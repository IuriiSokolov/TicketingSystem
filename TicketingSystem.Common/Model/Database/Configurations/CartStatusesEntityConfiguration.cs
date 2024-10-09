using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class CartStatusesEntityConfiguration : IEntityTypeConfiguration<CartStatusRow>
    {
        public void Configure(EntityTypeBuilder<CartStatusRow> builder)
        {
            builder.ToTable("CartStatuses");
            builder.Property(x => x.CartStatusId)
                .ValueGeneratedNever();
            builder.HasKey(x => x.CartStatusId);
        }
    }
}
