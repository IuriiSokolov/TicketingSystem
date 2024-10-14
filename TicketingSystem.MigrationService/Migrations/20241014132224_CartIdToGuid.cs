using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicketingSystem.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class CartIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "SeatStatusId",
                table: "SeatStatuses",
                newName: "TicketStatusId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "Tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "Carts",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CartStatus", "PaymentId", "PersonId" },
                values: new object[] { new Guid("5c30d45d-a693-4247-bb36-10908ecacc74"), 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 1,
                columns: new[] { "CartId", "Status" },
                values: new object[] { new Guid("5c30d45d-a693-4247-bb36-10908ecacc74"), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: new Guid("5c30d45d-a693-4247-bb36-10908ecacc74"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketStatusId",
                table: "SeatStatuses",
                newName: "SeatStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Seats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Carts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CartStatus", "PaymentId", "PersonId" },
                values: new object[] { 1, 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 1,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 1,
                column: "CartId",
                value: 1);
        }
    }
}
