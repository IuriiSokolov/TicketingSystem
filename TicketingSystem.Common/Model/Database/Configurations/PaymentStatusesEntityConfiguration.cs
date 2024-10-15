using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class PaymentStatusesEntityConfiguration : IEntityTypeConfiguration<PaymentStatusRow>
    {
        public void Configure(EntityTypeBuilder<PaymentStatusRow> builder)
        {
            builder.ToTable("PaymentStatuses");
            builder.Property(x => x.PaymentStatusId)
                .ValueGeneratedNever();
            builder.HasKey(x => x.PaymentStatusId);
        }
    }
}
