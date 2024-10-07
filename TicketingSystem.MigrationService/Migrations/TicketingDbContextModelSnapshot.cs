﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicketingSystem.Common.Context;

#nullable disable

namespace TicketingSystem.MigrationService.Migrations
{
    [DbContext(typeof(TicketingDbContext))]
    partial class TicketingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CartTicket", b =>
                {
                    b.Property<int>("CartsCartId")
                        .HasColumnType("integer");

                    b.Property<int>("TicketsTicketId")
                        .HasColumnType("integer");

                    b.HasKey("CartsCartId", "TicketsTicketId");

                    b.HasIndex("TicketsTicketId");

                    b.ToTable("CartTicket");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartId"));

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.HasKey("CartId");

                    b.HasIndex("PersonId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EventId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VenueId")
                        .HasColumnType("integer");

                    b.HasKey("EventId");

                    b.HasIndex("VenueId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Date = new DateTime(2024, 12, 30, 19, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Новогодний спектакль",
                            VenueId = 1
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            PaymentId = 1,
                            PaymentTime = new DateTime(2024, 11, 30, 19, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonId"));

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            ContactInfo = "testContact",
                            Name = "Юрий"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("SeatId");

                    b.HasIndex("EventId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            SeatId = 1,
                            Code = "1",
                            EventId = 1,
                            Status = 3
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.SeatStatusRow", b =>
                {
                    b.Property<int>("SeatStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SeatStatusId"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SeatStatusId");

                    b.ToTable("SeatStatuses");

                    b.HasData(
                        new
                        {
                            SeatStatusId = 1,
                            Status = "Free"
                        },
                        new
                        {
                            SeatStatusId = 2,
                            Status = "Booked"
                        },
                        new
                        {
                            SeatStatusId = 3,
                            Status = "Purchased"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TicketId"));

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<float>("PriceUsd")
                        .HasColumnType("real");

                    b.HasKey("TicketId");

                    b.HasIndex("PersonId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            PersonId = 1,
                            PriceUsd = 10f
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VenueId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VenueId");

                    b.ToTable("Venues");

                    b.HasData(
                        new
                        {
                            VenueId = 1,
                            Address = "ул. Зарафшан, 28",
                            Name = "Большой театр Навои"
                        });
                });

            modelBuilder.Entity("CartTicket", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Cart", null)
                        .WithMany()
                        .HasForeignKey("CartsCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketingSystem.Common.Model.Database.Ticket", null)
                        .WithMany()
                        .HasForeignKey("TicketsTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Cart", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Event", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Payment", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Ticket", "Ticket")
                        .WithOne("Payment")
                        .HasForeignKey("TicketingSystem.Common.Model.Database.Payment", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Seat", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Event", "Event")
                        .WithMany("Seats")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketingSystem.Common.Model.Database.Ticket", "Ticket")
                        .WithOne("Seat")
                        .HasForeignKey("TicketingSystem.Common.Model.Database.Seat", "SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Ticket", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Person", "Person")
                        .WithMany("Tickets")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Event", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Person", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Ticket", b =>
                {
                    b.Navigation("Payment");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Venue", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
