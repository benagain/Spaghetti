using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTest.Features
{
    /// <summary>
    /// The view model for ordering.
    /// </summary>
    public class OrderCommand
    {
        /// <summary>
        /// Public parameterless constructor required for ASP.Net MVC model binding.
        /// </summary>
        public OrderCommand()
        {
        }

        public OrderCommand(IEnumerable<MenuItemType> courses)
        {
            Courses = courses;
            NewCourses = courses.Select(
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
        }

        public IEnumerable<MenuItemType> Courses { get; }

        public Course[] NewCourses { get; set; }

        public int[] OrderedDishes
            => NewCourses
                .SelectMany(course => 
                    course.Dishes
                        .Where(dish => dish.Ordered)
                        .Select(dish => dish.Id))
                .ToArray();

        public int PaymentTypeId { get; set; }
    }

    public class Course
    {
        public string Name { get; set; }

        public Dish[] Dishes { get; set; }

        internal static Course[] Select()
        {
            throw new NotImplementedException();
        }
    }

    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Ordered { get; set; }
    }
}
