using System.Collections.Generic;

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
        }

        public IEnumerable<MenuItemType> Courses { get; }

        public int[] OrderedDishes { get; set; }

        public int PaymentTypeId { get; set; }
    }
}
