using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Attributes;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database.Configurations.Seeding
{
    public static class DBSeedingExtensions
    {
        public static void ApplySeeding(this ModelBuilder modelBuilder)
        {
            var cartId = Guid.NewGuid();
            var initialVenue = new Venue { VenueId = 1, Address = "ул. Зарафшан, 28", Name = "Большой театр Навои" };
            var initialSection = new Section { SectionId = 1, VenueId = 1 };
            var initialSeat = new Seat { SeatId = 1, SectionId = 1, RowNumber = 1, SeatType = SeatType.DesignatedSeat, EventId = 1 };
            var initialEvent = new Event { EventId = 1, Date = new DateTime(2024, 12, 31).ToUniversalTime(), Name = "Новогодний спектакль", VenueId = 1 };
            var initialTicket = new Ticket { TicketId = 1, CartId = cartId, PersonId = 1, EventId = 1, SeatId = 1, PriceCategoryId = 1, Status = TicketStatus.Purchased };
            var initialPriceCategory = new PriceCategory { PriceCategoryId = 1, PriceCategoryName = "Normal seat", EventId = 1, PriceUsd = 10 };
            var initialPerson = new Person { PersonId = 1, Name = "Юрий", ContactInfo = "testContact" };
            var initialPayment = new Payment { PaymentId = 1, PaymentStatus = PaymentStatus.Paid, PaymentTime = new DateTime(2024, 12, 1).ToUniversalTime() };
            var initialCart = new Cart { CartId = cartId, PersonId = 1, PaymentId = 1, CartStatus = CartStatus.Paid };

            modelBuilder.Entity<Venue>().HasData(initialVenue);
            modelBuilder.Entity<Section>().HasData(initialSection);
            modelBuilder.Entity<Seat>().HasData(initialSeat);
            modelBuilder.Entity<Event>().HasData(initialEvent);
            modelBuilder.Entity<Ticket>().HasData(initialTicket);
            modelBuilder.Entity<PriceCategory>().HasData(initialPriceCategory);
            modelBuilder.Entity<Person>().HasData(initialPerson);
            modelBuilder.Entity<Payment>().HasData(initialPayment);
            modelBuilder.Entity<Cart>().HasData(initialCart);

            modelBuilder.SeedEnum<TicketStatus, TicketStatusRow>();
            modelBuilder.SeedEnum<SeatType, SeatTypeRow>();
            modelBuilder.SeedEnum<CartStatus, CartStatusRow>();
            modelBuilder.SeedEnum<PaymentStatus, PaymentStatusRow>();
        }

        private static void SeedEnum<TEnum, TRow>(this ModelBuilder modelBuilder)
            where TEnum : Enum
            where TRow : class
        {
            var enumNames = Enum.GetNames(typeof(TEnum));
            var enumValues = Enum.GetValues(typeof(TEnum));
            var valueField = typeof(TRow).GetProperties()
                    .Single(f => f.GetCustomAttributes(false).Any(x => x is EnumValueAttribute));
            var nameField = typeof(TRow).GetProperties()
                    .Single(f => f.GetCustomAttributes(false).Any(x => x is EnumNameAttribute));

            for (int i = 0; i < enumNames.Length; i++)
            {
                var name = enumNames[i];
                var value = (int)enumValues.GetValue(i)!;
                var row = Activator.CreateInstance<TRow>();
                valueField.SetValue(row, value);
                nameField.SetValue(row, name);
                modelBuilder.Entity<TRow>().HasData(row);
            }
        }
    }
}
