using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemType> MenuItemTypes { get; set; }
    }
}
