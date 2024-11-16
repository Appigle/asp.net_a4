using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Asst4.Models;
using LEI_CHEN_Practice_Asst_4.Data;
using Asst4.Utilities;

namespace Asst4.Tests
{
    public class CRUDUtilitiesTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public CRUDUtilitiesTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<LEI_CHEN_Practice_Asst_4_DbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            _serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }

        private LEI_CHEN_Practice_Asst_4_DbContext GetNewContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<LEI_CHEN_Practice_Asst_4_DbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new LEI_CHEN_Practice_Asst_4_DbContext(options);
        }

        private void SeedTestData(string databaseName)
        {
            using var context = GetNewContext(databaseName);
            
            // Clear existing data
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Create test data
            var province = new Province 
            { 
                ID = new Random().Next(1000, 9999), // Random ID to avoid conflicts
                Name = "Ontario", 
                Code = "ON" 
            };
            context.Provinces.Add(province);

            var city = new City 
            { 
                ID = new Random().Next(1000, 9999),
                Name = "Toronto", 
                ProvCode = "ON" 
            };
            context.Cities.Add(city);

            var user = new User 
            { 
                ID = new Random().Next(1000, 9999),
                Name = "John Doe",
                Address = "123 Test St",
                CityName = "Toronto",
                PostalCode = "A1A1A1"
            };
            context.Users.Add(user);

            context.SaveChanges();

            // Set up CRUDUtilities with the new context
            var services = new ServiceCollection();
            services.AddDbContext<LEI_CHEN_Practice_Asst_4_DbContext>(options =>
                options.UseInMemoryDatabase(databaseName));
            var serviceProvider = services.BuildServiceProvider();
            CRUDUtilities.Initialize(serviceProvider);
        }

        [Fact]
        public void UserExists_ExistingUser_ReturnsTrue()
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            var result = CRUDUtilities.UserExists("John Doe");
            Assert.True(result);
        }

        [Fact]
        public void UserExists_NonExistingUser_ReturnsFalse()
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            var result = CRUDUtilities.UserExists("Jane Smith");
            Assert.False(result);
        }

        [Fact]
        public void UserExists_WithNullUserObject_ReturnsFalse()
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            User nullUser = null;
            var result = CRUDUtilities.UserExists(nullUser);
            Assert.False(result);
        }

        [Fact]
        public void ProvinceOfCity_ExistingCity_ReturnsProvinceName()
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            var result = CRUDUtilities.ProvinceOfCity("Toronto");
            Assert.Equal("Ontario", result);
        }

        [Fact]
        public void ProvinceOfCity_NonExistingCity_ReturnsEmptyString()
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            var result = CRUDUtilities.ProvinceOfCity("Shanghai"); // City that doesn't exist in test data
            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("John Doe", "ON", true)]
        [InlineData("John Doe", "BC", false)]
        [InlineData("Jane Smith", "ON", false)]
        public void LivesInProv_VariousScenarios_ReturnsExpectedResult(
            string userName, string provCode, bool expected)
        {
            string dbName = Guid.NewGuid().ToString();
            SeedTestData(dbName);
            var result = CRUDUtilities.LivesInProv(userName, provCode);
            Assert.Equal(expected, result);
        }
    }
}