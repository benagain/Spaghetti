using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentTest.Controllers;
using Xunit;

namespace RecruitmentTest.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var context = new RestaurantDbContext(builder.Options);
            await context.EnsureSeededAsync();

            HomeController controller = new HomeController(context);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task About()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var context = new RestaurantDbContext(builder.Options);
            await context.EnsureSeededAsync();

            HomeController controller = new HomeController(context);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.Equal("Davey P's Italian Restauranty was established in 2016 to produce Spaghetti Code.", result.ViewData["Message"]);
        }

        [Fact]
        public async Task Contact()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var context = new RestaurantDbContext(builder.Options);
            await context.EnsureSeededAsync();

            HomeController controller = new HomeController(context);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
