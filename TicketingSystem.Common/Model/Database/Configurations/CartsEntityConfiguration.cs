using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class CartsEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.CartId);
            builder.HasMany(x => x.Tickets)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId)
                .HasPrincipalKey(x => x.CartId);
        }
    }
}
