using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options)
            : base(options)
        {
        }

        private DbSet<MenuItem> _MenuItems { get; set; }

        public IQueryable<MenuItem> MenuItems => _MenuItems;

        public DbSet<MenuItemType> MenuItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keep the original table name
            modelBuilder.Entity<MenuItem>().ToTable("MenuItems");

            base.OnModelCreating(modelBuilder);
        }
    }
}
