using System;
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
            var menu = new[]
            {
                new MenuItemType(
                "Starter",
                new[]
                {
                    new MenuItem { Name = "Arancini", Price = 2.29m },
                    new MenuItem { Name = "Fonduta Formaggi", Price = 3.79m },
                    new MenuItem { Name = "Bruschetta", Price = 3.29m },
                    new MenuItem { Name = "Mixed Olives", Price = 2.75m },
                    new MenuItem { Name = "N'Duja Pizzette", Price = 3.10m },
                }),

                new MenuItemType(
                "Main",
                new[]
                {
                    new MenuItem { Name = "Spaghetti Bolognese", Price = 6.75m },
                    new MenuItem { Name = "Cheeseburger", Price = 6.99m },
                    new MenuItem { Name = "Lasagne", Price = 5.99m },
                    new MenuItem { Name = "Lobster and Crab Tortelli", Price = 14.99m },
                }),

                new MenuItemType(
                "Desert",
                new[]
                {
                    new MenuItem { Name = "Tiramisu", Price = 4.50m },
                    new MenuItem { Name = "Plum Tart", Price = 3.50m },
                    new MenuItem { Name = "Sorbet", Price = 1.99m }
                })
            };

            foreach (var itemType in menu)
            {
                // needs AddOrUpdate
                var existing = context
                    .MenuItemTypes
                    .Include(nameof(MenuItemType.Items))
                    .FirstOrDefault(x => x.Description == itemType.Description);

                if (existing == null)
                {
                    await context.MenuItemTypes.AddAsync(itemType);
                }
                else
                {
                    var missingDishes =
                        itemType.Items.Except(existing.Items, new MenuItemNameComparer())
                        .ToArray();

                    existing.AddItems(missingDishes);
                }
            }

            await context.SaveChangesAsync();

            return context;
        }
    }
}
