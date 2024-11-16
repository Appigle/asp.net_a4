using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asst4.Models;

namespace LEI_CHEN_Practice_Asst_4.Data
{
  public class LEI_CHEN_Practice_Asst_4_DbContext : DbContext
  {
    public LEI_CHEN_Practice_Asst_4_DbContext(DbContextOptions<LEI_CHEN_Practice_Asst_4_DbContext> options)
        : base(options)
    {
    }

    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BridgeClub> BridgeClubs { get; set; }
    public DbSet<UserBridgeClub> UserBridgeClubs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure relationships
      modelBuilder.Entity<City>()
          .HasOne(c => c.Province)
          .WithMany(p => p.Cities)
          .HasForeignKey(c => c.ProvCode)
          .HasPrincipalKey(p => p.Code)
          .IsRequired();

      modelBuilder.Entity<User>()
          .HasOne(u => u.City)
          .WithMany(c => c.Users)
          .HasForeignKey(u => u.CityName)
          .HasPrincipalKey(c => c.Name)
          .IsRequired();

      // Configure postal code cleaning
      modelBuilder.Entity<User>()
          .Property(u => u.PostalCode)
          .HasConversion(
              v => v.ToUpper().Replace(" ", ""),
              v => v
          );

      modelBuilder.Entity<Province>().HasData(
          new Province { ID = 1, Name = "Alberta", Code = "AB" },
          new Province { ID = 2, Name = "British Columbia", Code = "BC" },
          new Province { ID = 3, Name = "Manitoba", Code = "MB" },
          new Province { ID = 4, Name = "New Brunswick", Code = "NB" },
          new Province { ID = 5, Name = "Newfoundland and Labrador", Code = "NL" },
          new Province { ID = 6, Name = "Northwest Territories", Code = "NT" },
          new Province { ID = 7, Name = "Nova Scotia", Code = "NS" },
          new Province { ID = 8, Name = "Nunavut", Code = "NU" },
          new Province { ID = 9, Name = "Ontario", Code = "ON" },
          new Province { ID = 10, Name = "Prince Edward Island", Code = "PE" },
          new Province { ID = 11, Name = "Quebec", Code = "QC" },
          new Province { ID = 12, Name = "Saskatchewan", Code = "SK" },
          new Province { ID = 13, Name = "Yukon", Code = "YT" }
      );

      modelBuilder.Entity<User>().HasData(
        new User
        {
          ID = 1,
          Name = "Bill Anderson",
          Address = "220 Bay St.",
          CityName = "Toronto",
          PostalCode = "M6H2X4"
        },
        new User
        {
          ID = 2,
          Name = "Jilal Akbar",
          Address = "1007-33 Isabella St.",
          CityName =
        "Toronto",
          PostalCode = "M6H2Z9"
        },
        new User
        {
          ID = 3,
          Name = "Karishma Patel",
          Address = "2220 Seneca Blvd., .",
          CityName =
        "Brampton",
          PostalCode = "M6H7Y8"
        },
        new User
        {
          ID = 4,
          Name = "Mahinder Singh",
          Address = "10-660 Bolton Ave.",
          CityName =
        "Victoria",
          PostalCode = "D6H2C5"
        },
        new User
        {
          ID = 5,
          Name = "Ken Pu",
          Address = "99 Agness St.",
          CityName = "Montreal",
          PostalCode = "H1A0B1"
        }
      );

      modelBuilder.Entity<City>().HasData(
        new City { ID = 1, Name = "Ardrossan", ProvCode = "AB" },
        new City { ID = 2, Name = "Edmonton", ProvCode = "AB" },
        new City { ID = 3, Name = "Vancouver", ProvCode = "BC" },
        new City { ID = 4, Name = "Victoria", ProvCode = "BC" },
        new City { ID = 5, Name = "Winnipeg", ProvCode = "MB" },
        new City { ID = 6, Name = "Woodstock", ProvCode = "NB" },
        new City { ID = 7, Name = "Whitbourne", ProvCode = "NL" },
        new City { ID = 8, Name = "Yellowknife", ProvCode = "NT" },
        new City { ID = 9, Name = "Halifax", ProvCode = "NS" },
        new City { ID = 10, Name = "Arviat", ProvCode = "NU" },
        new City { ID = 11, Name = "Brampton", ProvCode = "ON" },
        new City { ID = 12, Name = "Toronto", ProvCode = "ON" },
        new City { ID = 13, Name = "Kensington", ProvCode = "PE" },
        new City { ID = 14, Name = "Montreal", ProvCode = "QC" },
        new City { ID = 15, Name = "Saskatoon", ProvCode = "SK" },
        new City { ID = 16, Name = "Mayo", ProvCode = "YT" }
      );

      modelBuilder.Entity<BridgeClub>()
            .HasOne(bc => bc.City)
            .WithMany()
            .HasForeignKey(bc => bc.CityName)
            .HasPrincipalKey(c => c.Name)
            .IsRequired();

      modelBuilder.Entity<UserBridgeClub>()
          .HasKey(ubc => new { ubc.UserID, ubc.ClubID });

      modelBuilder.Entity<UserBridgeClub>()
          .HasOne(ubc => ubc.User)
          .WithMany(u => u.UserBridgeClubs)
          .HasForeignKey(ubc => ubc.UserID);

      modelBuilder.Entity<UserBridgeClub>()
          .HasOne(ubc => ubc.BridgeClub)
          .WithMany(bc => bc.UserBridgeClubs)
          .HasForeignKey(ubc => ubc.ClubID);

      // Ensure every club has at least one member
      modelBuilder.Entity<BridgeClub>()
          .HasMany(bc => bc.UserBridgeClubs)
          .WithOne(ubc => ubc.BridgeClub)
          .IsRequired();
    }
  }
}
