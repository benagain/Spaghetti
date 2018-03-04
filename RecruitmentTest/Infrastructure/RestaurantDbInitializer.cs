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
                    new MenuItem ( name: "Arancini", price: 2.29m ),
                    new MenuItem ( name: "Fonduta Formaggi", price: 3.79m ),
                    new MenuItem ( name: "Bruschetta", price: 3.29m ),
                    new MenuItem ( name: "Mixed Olives", price: 2.75m ),
                    new MenuItem ( name: "N'Duja Pizzette", price: 3.10m ),
                }),

                new MenuItemType(
                "Main",
                new[]
                {
                    new MenuItem ( name: "Spaghetti Bolognese", price: 6.75m ),
                    new MenuItem ( name: "Cheeseburger", price: 6.99m ),
                    new MenuItem ( name: "Lasagne", price: 5.99m ),
                    new MenuItem ( name: "Lobster and Crab Tortelli", price: 14.99m ),
                }),

                new MenuItemType(
                "Desert",
                new[]
                {
                    new MenuItem ( name: "Tiramisu", price: 4.50m ),
                    new MenuItem ( name: "Plum Tart", price: 3.50m ),
                    new MenuItem ( name: "Sorbet", price: 1.99m ),
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
