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

        public OrderCommand(Course[] courses)
        {
            Courses = courses;
        }

        public Course[] Courses { get; set; }

        public int[] OrderedDishes
            => Courses
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
    }

    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Ordered { get; set; }
    }
}
