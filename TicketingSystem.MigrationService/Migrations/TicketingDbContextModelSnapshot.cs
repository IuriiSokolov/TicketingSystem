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

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Cart", b =>
                {
                    b.Property<Guid>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CartStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.HasKey("CartId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("PersonId");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            CartId = new Guid("4fd9f65b-fdd7-41c3-af09-1b5c4d254fac"),
                            CartStatus = 1,
                            PaymentId = 1,
                            PersonId = 1
                        },
                        new
                        {
                            CartId = new Guid("e7405abe-7e3f-4769-8226-2fcac7e946cf"),
                            CartStatus = 0,
                            PersonId = 1
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.EnumEntities.CartStatusRow", b =>
                {
                    b.Property<int>("CartStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CartStatusId");

                    b.ToTable("CartStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            CartStatusId = 0,
                            Status = "NotPaid"
                        },
                        new
                        {
                            CartStatusId = 1,
                            Status = "Paid"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.EnumEntities.PaymentStatusRow", b =>
                {
                    b.Property<int>("PaymentStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PaymentStatusId");

                    b.ToTable("PaymentStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            PaymentStatusId = 0,
                            Status = "Pending"
                        },
                        new
                        {
                            PaymentStatusId = 1,
                            Status = "Paid"
                        },
                        new
                        {
                            PaymentStatusId = 2,
                            Status = "Failed"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.EnumEntities.SeatTypeRow", b =>
                {
                    b.Property<int>("SeatTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("SeatType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SeatTypeId");

                    b.ToTable("SeatTypes", (string)null);

                    b.HasData(
                        new
                        {
                            SeatTypeId = 0,
                            SeatType = "DesignatedSeat"
                        },
                        new
                        {
                            SeatTypeId = 1,
                            SeatType = "GeneralAdmission"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.EnumEntities.TicketStatusRow", b =>
                {
                    b.Property<int>("TicketStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TicketStatusId");

                    b.ToTable("SeatStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            TicketStatusId = 0,
                            Status = "Free"
                        },
                        new
                        {
                            TicketStatusId = 1,
                            Status = "Booked"
                        },
                        new
                        {
                            TicketStatusId = 2,
                            Status = "Purchased"
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Event", b =>
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

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PaymentId"));

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("PaymentTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            PaymentId = 1,
                            CartId = new Guid("4fd9f65b-fdd7-41c3-af09-1b5c4d254fac"),
                            PaymentStatus = 1,
                            PaymentTime = new DateTime(2024, 11, 30, 19, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Person", b =>
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

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.PriceCategory", b =>
                {
                    b.Property<int>("PriceCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PriceCategoryId"));

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("PriceCategoryDescription")
                        .HasColumnType("text");

                    b.Property<string>("PriceCategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("PriceUsd")
                        .HasColumnType("real");

                    b.HasKey("PriceCategoryId");

                    b.HasIndex("EventId");

                    b.ToTable("PriceCategories");

                    b.HasData(
                        new
                        {
                            PriceCategoryId = 1,
                            EventId = 1,
                            PriceCategoryName = "Normal seat",
                            PriceUsd = 10f
                        },
                        new
                        {
                            PriceCategoryId = 2,
                            EventId = 1,
                            PriceCategoryName = "VIP seat",
                            PriceUsd = 15f
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SeatId"));

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int?>("RowNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SeatType")
                        .HasColumnType("integer");

                    b.Property<int?>("SectionId")
                        .HasColumnType("integer");

                    b.HasKey("SeatId");

                    b.HasIndex("EventId");

                    b.HasIndex("SectionId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            SeatId = 1,
                            EventId = 1,
                            RowNumber = 1,
                            SeatType = 0,
                            SectionId = 1
                        },
                        new
                        {
                            SeatId = 2,
                            EventId = 1,
                            RowNumber = 2,
                            SeatType = 1,
                            SectionId = 1
                        },
                        new
                        {
                            SeatId = 3,
                            EventId = 1,
                            RowNumber = 3,
                            SeatType = 0,
                            SectionId = 1
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SectionId"));

                    b.Property<int>("VenueId")
                        .HasColumnType("integer");

                    b.HasKey("SectionId");

                    b.HasIndex("VenueId");

                    b.ToTable("Sections");

                    b.HasData(
                        new
                        {
                            SectionId = 1,
                            VenueId = 1
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TicketId"));

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uuid");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("PriceCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("TicketId");

                    b.HasIndex("CartId");

                    b.HasIndex("EventId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PriceCategoryId");

                    b.HasIndex("SeatId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            CartId = new Guid("4fd9f65b-fdd7-41c3-af09-1b5c4d254fac"),
                            EventId = 1,
                            PersonId = 1,
                            PriceCategoryId = 1,
                            SeatId = 1,
                            Status = 2
                        },
                        new
                        {
                            TicketId = 2,
                            EventId = 1,
                            PriceCategoryId = 1,
                            SeatId = 2,
                            Status = 0
                        },
                        new
                        {
                            TicketId = 3,
                            EventId = 1,
                            PriceCategoryId = 2,
                            SeatId = 3,
                            Status = 0
                        });
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Venue", b =>
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

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Cart", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Payment", "Payment")
                        .WithOne("Cart")
                        .HasForeignKey("TicketingSystem.Common.Model.Database.Entities.Cart", "PaymentId");

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Event", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.PriceCategory", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Seat", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Section", "Section")
                        .WithMany("Seats")
                        .HasForeignKey("SectionId");

                    b.Navigation("Event");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Section", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Venue", "Venue")
                        .WithMany("Sections")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Ticket", b =>
                {
                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Cart", "Cart")
                        .WithMany("Tickets")
                        .HasForeignKey("CartId");

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Person", "Person")
                        .WithMany("Tickets")
                        .HasForeignKey("PersonId");

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.PriceCategory", "PriceCategory")
                        .WithMany("Tickets")
                        .HasForeignKey("PriceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketingSystem.Common.Model.Database.Entities.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Event");

                    b.Navigation("Person");

                    b.Navigation("PriceCategory");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Cart", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Event", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Payment", b =>
                {
                    b.Navigation("Cart");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Person", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.PriceCategory", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Section", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("TicketingSystem.Common.Model.Database.Entities.Venue", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
