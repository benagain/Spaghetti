using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest.Features
{
    public class OrderQueryHandler
    {
        private readonly RestaurantDbContext context;

        public OrderQueryHandler(RestaurantDbContext context)
        {
            this.context = context;
        }

        public OrderCommand Handle()
            => new OrderCommand(
                context
                    .MenuItemTypes
                    .Include(x => x.Items));
    }
}
