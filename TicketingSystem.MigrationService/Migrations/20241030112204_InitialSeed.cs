using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketingSystem.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (Environment.GetEnvironmentVariable("IntegrationTests") == "true")
                return;
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CartId", "PaymentStatus", "PaymentTime" },
                values: new object[] { 1, new Guid("a29e759f-172b-4390-b1fb-a5956e60d04b"), 1, new DateTime(2024, 11, 30, 19, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "ContactInfo", "Name" },
                values: new object[] { 1, "testContact", "Юрий" });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "Address", "Description", "Name" },
                values: new object[] { 1, "ул. Зарафшан, 28", null, "Большой театр Навои" });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CartStatus", "PaymentId", "PersonId" },
                values: new object[,]
                {
                    { new Guid("892c9499-7738-436a-b233-ed55bc6e40e6"), 0, null, 1 },
                    { new Guid("a29e759f-172b-4390-b1fb-a5956e60d04b"), 1, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Date", "Description", "Name", "VenueId" },
                values: new object[] { 1, new DateTime(2024, 12, 30, 19, 0, 0, 0, DateTimeKind.Utc), null, "Новогодний спектакль", 1 });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "SectionId", "VenueId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "PriceCategories",
                columns: new[] { "PriceCategoryId", "EventId", "PriceCategoryDescription", "PriceCategoryName", "PriceUsd" },
                values: new object[,]
                {
                    { 1, 1, null, "Normal seat", 10f },
                    { 2, 1, null, "VIP seat", 15f }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "EventId", "RowNumber", "SeatType", "SectionId" },
                values: new object[,]
                {
                    { 1, 1, 1, 0, 1 },
                    { 2, 1, 2, 1, 1 },
                    { 3, 1, 3, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "CartId", "EventId", "PersonId", "PriceCategoryId", "SeatId", "Status" },
                values: new object[,]
                {
                    { 1, new Guid("a29e759f-172b-4390-b1fb-a5956e60d04b"), 1, 1, 1, 1, 2 },
                    { 2, null, 1, null, 1, 2, 0 },
                    { 3, null, 1, null, 2, 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: new Guid("892c9499-7738-436a-b233-ed55bc6e40e6"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: new Guid("a29e759f-172b-4390-b1fb-a5956e60d04b"));

            migrationBuilder.DeleteData(
                table: "PriceCategories",
                keyColumn: "PriceCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PriceCategories",
                keyColumn: "PriceCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 1);
        }
    }
}
