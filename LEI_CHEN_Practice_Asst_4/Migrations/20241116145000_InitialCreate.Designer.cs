﻿// <auto-generated />
using LEI_CHEN_Practice_Asst_4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LEI_CHEN_Practice_Asst_4.Migrations
{
    [DbContext(typeof(LEI_CHEN_Practice_Asst_4_DbContext))]
    [Migration("20241116145000_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Asst4.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProvCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ProvCode");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Ardrossan",
                            ProvCode = "AB"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Edmonton",
                            ProvCode = "AB"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Vancouver",
                            ProvCode = "BC"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Victoria",
                            ProvCode = "BC"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Winnipeg",
                            ProvCode = "MB"
                        },
                        new
                        {
                            ID = 6,
                            Name = "Woodstock",
                            ProvCode = "NB"
                        },
                        new
                        {
                            ID = 7,
                            Name = "Whitbourne",
                            ProvCode = "NL"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Yellowknife",
                            ProvCode = "NT"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Halifax",
                            ProvCode = "NS"
                        },
                        new
                        {
                            ID = 10,
                            Name = "Arviat",
                            ProvCode = "NU"
                        },
                        new
                        {
                            ID = 11,
                            Name = "Brampton",
                            ProvCode = "ON"
                        },
                        new
                        {
                            ID = 12,
                            Name = "Toronto",
                            ProvCode = "ON"
                        },
                        new
                        {
                            ID = 13,
                            Name = "Kensington",
                            ProvCode = "PE"
                        },
                        new
                        {
                            ID = 14,
                            Name = "Montreal",
                            ProvCode = "QC"
                        },
                        new
                        {
                            ID = 15,
                            Name = "Saskatoon",
                            ProvCode = "SK"
                        },
                        new
                        {
                            ID = 16,
                            Name = "Mayo",
                            ProvCode = "YT"
                        });
                });

            modelBuilder.Entity("Asst4.Province", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Provinces");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Code = "AB",
                            Name = "Alberta"
                        },
                        new
                        {
                            ID = 2,
                            Code = "BC",
                            Name = "British Columbia"
                        },
                        new
                        {
                            ID = 3,
                            Code = "MB",
                            Name = "Manitoba"
                        },
                        new
                        {
                            ID = 4,
                            Code = "NB",
                            Name = "New Brunswick"
                        },
                        new
                        {
                            ID = 5,
                            Code = "NL",
                            Name = "Newfoundland and Labrador"
                        },
                        new
                        {
                            ID = 6,
                            Code = "NT",
                            Name = "Northwest Territories"
                        },
                        new
                        {
                            ID = 7,
                            Code = "NS",
                            Name = "Nova Scotia"
                        },
                        new
                        {
                            ID = 8,
                            Code = "NU",
                            Name = "Nunavut"
                        },
                        new
                        {
                            ID = 9,
                            Code = "ON",
                            Name = "Ontario"
                        },
                        new
                        {
                            ID = 10,
                            Code = "PE",
                            Name = "Prince Edward Island"
                        },
                        new
                        {
                            ID = 11,
                            Code = "QC",
                            Name = "Quebec"
                        },
                        new
                        {
                            ID = 12,
                            Code = "SK",
                            Name = "Saskatchewan"
                        },
                        new
                        {
                            ID = 13,
                            Code = "YT",
                            Name = "Yukon"
                        });
                });

            modelBuilder.Entity("Asst4.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CityName");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Address = "220 Bay St.",
                            CityName = "Toronto",
                            Name = "Bill Anderson",
                            PostalCode = "M6H2X4"
                        },
                        new
                        {
                            ID = 2,
                            Address = "1007-33 Isabella St.",
                            CityName = "Toronto",
                            Name = "Jilal Akbar",
                            PostalCode = "M6H2Z9"
                        },
                        new
                        {
                            ID = 3,
                            Address = "2220 Seneca Blvd., .",
                            CityName = "Brampton",
                            Name = "Karishma Patel",
                            PostalCode = "M6H7Y8"
                        },
                        new
                        {
                            ID = 4,
                            Address = "10-660 Bolton Ave.",
                            CityName = "Victoria",
                            Name = "Mahinder Singh",
                            PostalCode = "D6H2C5"
                        },
                        new
                        {
                            ID = 5,
                            Address = "99 Agness St.",
                            CityName = "Montreal",
                            Name = "Ken Pu",
                            PostalCode = "H1A0B1"
                        });
                });

            modelBuilder.Entity("Asst4.City", b =>
                {
                    b.HasOne("Asst4.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvCode")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("Asst4.User", b =>
                {
                    b.HasOne("Asst4.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityName")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Asst4.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Asst4.Province", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
