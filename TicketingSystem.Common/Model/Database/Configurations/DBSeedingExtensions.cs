using Microsoft.EntityFrameworkCore;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database.Configurations
{
    public static class DBSeedingExtensions
    {
        public static void ApplySeeding(this ModelBuilder modelBuilder)
        {
            var initialVenue = new Venue { VenueId = 1, Address = "ул. Зарафшан, 28", Name = "Большой театр Навои" };
            var initialSection = new Section { SectionId = 1, VenueId = 1 };
            var initialSeat = new Seat { SeatId = 1, SectionId = 1, RowNumber = 1, SeatType = SeatType.DesignatedSeat, Status = SeatStatus.Purchased, EventId = 1 };
            var initialEvent = new Event { EventId = 1, Date = new DateTime(2024, 12, 31).ToUniversalTime(), Name = "Новогодний спектакль", VenueId = 1 };
            var initialTicket = new Ticket { TicketId = 1, CartId = 1, PersonId = 1, EventId = 1, SeatId = 1, PriceCategoryId = 1 };
            var initialPriceCategory = new PriceCategory { PriceCategoryId = 1, PriceCategoryName = "Normal seat", EventId = 1, PriceUsd = 10 };
            var initialPerson = new Person { PersonId = 1, Name = "Юрий", ContactInfo = "testContact" };
            var initialPayment = new Payment { PaymentId = 1, PaymentTime = new DateTime(2024, 12, 1).ToUniversalTime() };
            var initialCart = new Cart { CartId = 1, PersonId = 1, PaymentId = 1, CartStatus = CartStatus.Payed };

            modelBuilder.Entity<Venue>().HasData(initialVenue);
            modelBuilder.Entity<Section>().HasData(initialSection);
            modelBuilder.Entity<Seat>().HasData(initialSeat);
            modelBuilder.Entity<Event>().HasData(initialEvent);
            modelBuilder.Entity<Ticket>().HasData(initialTicket);
            modelBuilder.Entity<PriceCategory>().HasData(initialPriceCategory);
            modelBuilder.Entity<Person>().HasData(initialPerson);
            modelBuilder.Entity<Payment>().HasData(initialPayment);
            modelBuilder.Entity<Cart>().HasData(initialCart);

            var seatStatusValues = Enum.GetValues<SeatStatus>().Select(x => (int)x).ToList();
            for (int i = seatStatusValues.Min(); i <= seatStatusValues.Max(); i++)
            {
                var seatStatus = new SeatStatusRow
                {
                    SeatStatusId = i,
                    Status = Enum.GetName((SeatStatus)i)!
                };
                modelBuilder.Entity<SeatStatusRow>().HasData(seatStatus);
            }

            var seatTypeValues = Enum.GetValues<SeatType>().Select(x => (int)x).ToList();
            for (int i = seatTypeValues.Min(); i <= seatTypeValues.Max(); i++)
            {
                var seatType = new SeatTypeRow
                {
                    SeatTypeId = i,
                    SeatType = Enum.GetName((SeatType)i)!
                };
                modelBuilder.Entity<SeatTypeRow>().HasData(seatType);
            }

            var cartStatusValues = Enum.GetValues<CartStatus>().Select(x => (int)x).ToList();
            for (int i = cartStatusValues.Min(); i <= cartStatusValues.Max(); i++)
            {
                var cartStatus = new CartStatusRow
                {
                    CartStatusId = i,
                    Status = Enum.GetName((CartStatus)i)!
                };
                modelBuilder.Entity<CartStatusRow>().HasData(cartStatus);
            }
        }
    }
}
