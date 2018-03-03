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
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return host;
        }

        public static async Task<RestaurantDbContext> EnsureSeededAsync(this RestaurantDbContext context)
        {
            var menuItemTypes = new List<MenuItemType>();

            menuItemTypes.Add(new MenuItemType { Id = 1, Description = "Starter" });
            menuItemTypes.Add(new MenuItemType { Id = 2, Description = "Main" });
            menuItemTypes.Add(new MenuItemType { Id = 3, Description = "Desert" });

            var unseededMenuItemTypes = menuItemTypes.Except(context.MenuItemTypes);
            await context.AddRangeAsync(unseededMenuItemTypes);

            var menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem { Id = 1, MenuItemTypeId = 1, Name = "Arancini", Price = 2.29m });
            menuItems.Add(new MenuItem { Id = 2, MenuItemTypeId = 1, Name = "Fonduta Formaggi", Price = 3.79m });
            menuItems.Add(new MenuItem { Id = 3, MenuItemTypeId = 1, Name = "Bruschetta", Price = 3.29m });
            menuItems.Add(new MenuItem { Id = 4, MenuItemTypeId = 1, Name = "Mixed Olives", Price = 2.75m });
            menuItems.Add(new MenuItem { Id = 5, MenuItemTypeId = 1, Name = "N'Duja Pizzette", Price = 3.10m });
            menuItems.Add(new MenuItem { Id = 6, MenuItemTypeId = 2, Name = "Spaghetti Bolognese", Price = 6.75m });
            menuItems.Add(new MenuItem { Id = 7, MenuItemTypeId = 2, Name = "Cheeseburger", Price = 6.99m });
            menuItems.Add(new MenuItem { Id = 8, MenuItemTypeId = 2, Name = "Lasagne", Price = 5.99m });
            menuItems.Add(new MenuItem { Id = 9, MenuItemTypeId = 2, Name = "Lobster and Crab Tortelli", Price = 14.99m });
            menuItems.Add(new MenuItem { Id = 10, MenuItemTypeId = 3, Name = "Tiramisu", Price = 4.50m });
            menuItems.Add(new MenuItem { Id = 11, MenuItemTypeId = 3, Name = "Plum Tart", Price = 3.50m });
            menuItems.Add(new MenuItem { Id = 12, MenuItemTypeId = 3, Name = "Sorbet", Price = 1.99m });

            var unseededMenuItems = menuItems.Except(context.MenuItems);
            await context.AddRangeAsync(unseededMenuItems);

            await context.SaveChangesAsync();

            return context;
        }
    }
}