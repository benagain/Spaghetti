using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RecruitmentTest
{
    public static class PrepareDatabaseExtensions
    {
        public static async Task<IWebHost> PrepareDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var context = serviceProvider.GetRequiredService<RestaurantDbContext>();
                    await context.Database.MigrateAsync();
                    await context.EnsureSeededAsync();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<RestaurantDbContext>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return host;
        }

        public static async Task<RestaurantDbContext> EnsureSeededAsync(this RestaurantDbContext context)
        {
            //var starter = new MenuItemType { Description = "Starter" };
            //var main = new MenuItemType { Description = "Main" };
            //var desert = new MenuItemType { Description = "Desert" };

            //var unseededMenuItemTypes =
            //    new[] { starter, main, desert }
            //    .Except(context.MenuItemTypes);

            //await context.AddRangeAsync(unseededMenuItemTypes);
            //await context.SaveChangesAsync();

            //var menuItems = new List<MenuItem>
            //{
            //    new MenuItem { MenuItemTypeId = starter.Id, Name = "Arancini", Price = 2.29m },
            //    new MenuItem { MenuItemTypeId = starter.Id, Name = "Fonduta Formaggi", Price = 3.79m },
            //    new MenuItem { MenuItemTypeId = starter.Id, Name = "Bruschetta", Price = 3.29m },
            //    new MenuItem { MenuItemTypeId = starter.Id, Name = "Mixed Olives", Price = 2.75m },
            //    new MenuItem { MenuItemTypeId = starter.Id, Name = "N'Duja Pizzette", Price = 3.10m },
            //    new MenuItem { MenuItemTypeId = main.Id, Name = "Spaghetti Bolognese", Price = 6.75m },
            //    new MenuItem { MenuItemTypeId = main.Id, Name = "Cheeseburger", Price = 6.99m },
            //    new MenuItem { MenuItemTypeId = main.Id, Name = "Lasagne", Price = 5.99m },
            //    new MenuItem { MenuItemTypeId = main.Id, Name = "Lobster and Crab Tortelli", Price = 14.99m },
            //    new MenuItem { MenuItemTypeId = desert.Id, Name = "Tiramisu", Price = 4.50m },
            //    new MenuItem { MenuItemTypeId = desert.Id, Name = "Plum Tart", Price = 3.50m },
            //    new MenuItem { MenuItemTypeId = desert.Id, Name = "Sorbet", Price = 1.99m }
            //};

            //var unseededMenuItems = menuItems.Except(context.MenuItems);
            //await context.AddRangeAsync(unseededMenuItems);

            await context.SaveChangesAsync();

            return context;
        }
    }
}
