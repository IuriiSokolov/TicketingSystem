using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    internal class SeatStatusesEntityConfiguration : IEntityTypeConfiguration<TicketStatusRow>
    {
        public void Configure(EntityTypeBuilder<TicketStatusRow> builder)
        {
            builder.ToTable("SeatStatuses");
            builder.Property(x => x.TicketStatusId)
                .ValueGeneratedNever();
            builder.HasKey(x => x.TicketStatusId);
        }
    }
}
