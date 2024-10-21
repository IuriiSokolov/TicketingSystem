using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class SeatTypesEntityConfiguration : IEntityTypeConfiguration<SeatTypeRow>
    {
        public void Configure(EntityTypeBuilder<SeatTypeRow> builder)
        {
            builder.ToTable("SeatTypes");
            builder.Property(x => x.SeatTypeId)
                .ValueGeneratedNever();
            builder.HasKey(x => x.SeatTypeId);
        }
    }
}
