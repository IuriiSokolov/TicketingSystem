using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class CartsTicketsEntityConfiguration : IEntityTypeConfiguration<CartTicket>
    {
        public void Configure(EntityTypeBuilder<CartTicket> builder)
        {
            builder.HasKey(x => new { x.CartId, x.TicketId });
        }
    }
}
