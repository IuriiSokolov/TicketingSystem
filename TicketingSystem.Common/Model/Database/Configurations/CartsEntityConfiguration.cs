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
                .WithMany(x => x.Carts)
                .UsingEntity<CartTicket>(
                l => l.HasOne(x => x.Ticket).WithMany().HasForeignKey(x => x.TicketId).HasPrincipalKey(x => x.TicketId),
                r => r.HasOne(x => x.Cart).WithMany().HasForeignKey(x => x.CartId).HasPrincipalKey(x => x.CartId),
                j => j.HasKey(x => new { x.CartId, x.TicketId }));
        }
    }
}
