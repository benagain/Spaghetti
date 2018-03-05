using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        {
            var allMenuItems = context
                               .MenuItemTypes
                               .Include(x => x.Items);

            var courses = allMenuItems.Select(
                course => new Course
                {
                    Name = course.Description,
                    Dishes = course.Items.Select(
                        dish => new Dish
                        {
                            Id = dish.Id,
                            Name = dish.Name,
                            Price = dish.Price
                        }).ToArray()
                }).ToArray();

            return new OrderCommand(courses);
        }
    }
}
