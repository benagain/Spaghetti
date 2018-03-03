using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest.Features
{
    public class Menu
    {
        public IEnumerable<MenuItemType> Courses { get; set; }

        public class Handler
        {
            private readonly RestaurantDbContext context;

            public Handler(RestaurantDbContext context)
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
}
