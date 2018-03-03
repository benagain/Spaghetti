using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest.Tests.Helpers
{
    public class InMemoryRestaurantDbContextBuilder
    {
        private readonly string databaseName;

        public InMemoryRestaurantDbContextBuilder(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public RestaurantDbContext Build()
        {
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase(databaseName);
            return new RestaurantDbContext(builder.Options);
        }
    }
}
