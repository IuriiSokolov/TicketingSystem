using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TicketingSystem.Common.Model.Attributes;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Entities.EnumEntities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database.Configurations.Seeding
{
    public static class DBSeedingExtensions
    {
        public static void ApplyEnumSeeding(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedEnum<TicketStatus, TicketStatusRow>();
            modelBuilder.SeedEnum<SeatType, SeatTypeRow>();
            modelBuilder.SeedEnum<CartStatus, CartStatusRow>();
            modelBuilder.SeedEnum<PaymentStatus, PaymentStatusRow>();
        }
        public static void ApplySeeding(this ModelBuilder modelBuilder)
        {
            var paidCartId = Guid.NewGuid();
            var emptyCartId = Guid.NewGuid();
            var venue = new Venue { VenueId = 1, Address = "ул. Зарафшан, 28", Name = "Большой театр Навои" };
            var section = new Section { SectionId = 1, VenueId = 1 };
            var normalSeat = new Seat { SeatId = 1, SectionId = 1, RowNumber = 1, SeatType = SeatType.DesignatedSeat, EventId = 1 };
            var areaSeat = new Seat { SeatId = 2, SectionId = 1, RowNumber = 2, SeatType = SeatType.GeneralAdmission, EventId = 1 };
            var vipSeat = new Seat { SeatId = 3, SectionId = 1, RowNumber = 3, SeatType = SeatType.DesignatedSeat, EventId = 1 };
            var initialEvent = new Event { EventId = 1, Date = new DateTime(2024, 12, 31).ToUniversalTime(), Name = "Новогодний спектакль", VenueId = 1 };
            var paidTicket = new Ticket { TicketId = 1, CartId = paidCartId, PersonId = 1, EventId = 1, SeatId = 1, PriceCategoryId = 1, Status = TicketStatus.Purchased };
            var freeAreaTicket = new Ticket { TicketId = 2, EventId = 1, SeatId = 2, PriceCategoryId = 1, Status = TicketStatus.Free };
            var freeVipTicket = new Ticket { TicketId = 3, EventId = 1, SeatId = 3, PriceCategoryId = 2, Status = TicketStatus.Free };
            var normalPriceCategory = new PriceCategory { PriceCategoryId = 1, PriceCategoryName = "Normal seat", EventId = 1, PriceUsd = 10 };
            var vipPriceCategory = new PriceCategory { PriceCategoryId = 2, PriceCategoryName = "VIP seat", EventId = 1, PriceUsd = 15 };
            var person = new Person { PersonId = 1, Name = "Юрий", ContactInfo = "testContact" };
            var paidPayment = new Payment { PaymentId = 1, PaymentStatus = PaymentStatus.Paid, PaymentTime = new DateTime(2024, 12, 1).ToUniversalTime(), CartId = paidCartId };
            var paidCart = new Cart { CartId = paidCartId, PersonId = 1, PaymentId = 1, CartStatus = CartStatus.Paid };
            var emptyCart = new Cart { CartId = emptyCartId, PersonId = 1, CartStatus = CartStatus.NotPaid };

            modelBuilder.Entity<Venue>().HasData(venue);
            modelBuilder.Entity<Section>().HasData(section);
            modelBuilder.Entity<Seat>().HasData(normalSeat, areaSeat, vipSeat);
            modelBuilder.Entity<Event>().HasData(initialEvent);
            modelBuilder.Entity<Ticket>().HasData(paidTicket, freeAreaTicket, freeVipTicket);
            modelBuilder.Entity<PriceCategory>().HasData(normalPriceCategory, vipPriceCategory);
            modelBuilder.Entity<Person>().HasData(person);
            modelBuilder.Entity<Payment>().HasData(paidPayment);
            modelBuilder.Entity<Cart>().HasData(paidCart, emptyCart);
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
