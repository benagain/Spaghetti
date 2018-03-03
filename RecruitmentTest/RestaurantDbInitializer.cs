using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecruitmentTest
{
    public class RestaurantDbInitializer : DropCreateDatabaseAlways<RestaurantDbContext>
    {
        protected override void Seed(RestaurantDbContext context)
        {
            var menuItemTypes = new List<MenuItemType>();

            menuItemTypes.Add(new MenuItemType { Id = 1, Description = "Starter" });
            menuItemTypes.Add(new MenuItemType { Id = 2, Description = "Main" });
            menuItemTypes.Add(new MenuItemType { Id = 3, Description = "Desert" });

            foreach (var menuItemType in menuItemTypes)
                context.MenuItemTypes.Add(menuItemType);

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

            foreach (var menuItem in menuItems)
                context.MenuItems.Add(menuItem);

            base.Seed(context);
        }
    }
}