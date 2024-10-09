using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public class PersonsEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.PersonId);
            builder.HasMany(x => x.Tickets)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .HasPrincipalKey(x => x.PersonId);
        }
    }
}
