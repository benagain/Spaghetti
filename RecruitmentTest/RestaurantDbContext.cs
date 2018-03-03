using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecruitmentTest
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext() 
        {
            Database.SetInitializer(new RestaurantDbInitializer());

            this.Configuration.LazyLoadingEnabled = false;
        } 

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemType> MenuItemTypes { get; set; }      
    }
}