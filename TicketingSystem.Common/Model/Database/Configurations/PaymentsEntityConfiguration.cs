using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;
namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class PaymentsEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.PaymentId);
            builder.HasOne(e => e.Cart)
                .WithOne(e => e.Payment)
                .HasForeignKey<Cart>(x => x.PaymentId)
                .IsRequired(false);
        }
    }
}
