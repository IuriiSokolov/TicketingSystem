using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    internal class SeatStatusesEntityConfiguration : IEntityTypeConfiguration<SeatStatusRow>
    {
        public void Configure(EntityTypeBuilder<SeatStatusRow> builder)
        {
            builder.ToTable("SeatStatuses");
            builder.Property(x => x.SeatStatusId)
                .ValueGeneratedNever();
            builder.HasKey(x => x.SeatStatusId);
        }
    }
}
