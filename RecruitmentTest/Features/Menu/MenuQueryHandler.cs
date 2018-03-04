using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest.Features
{
    public class MenuQueryHandler
    {
        private readonly RestaurantDbContext context;

        public MenuQueryHandler(RestaurantDbContext context)
        {
            this.context = context;
        }

        public Menu Handle()
            => new Menu
            {
                Courses = context
                    .MenuItemTypes
                    .Include(x => x.Items),
            };
    }
}
