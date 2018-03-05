using RecruitmentTest.Features;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTest.Tests.Helpers
{
    public class OrderBuilder
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();
        private int PaymentTypeId;

        public OrderBuilder(MenuItem item)
        {
            WithItem(item);
            WithDebitCard();
        }

        public OrderBuilder(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
                WithItem(item);

            WithDebitCard();
        }

        public OrderBuilder WithItem(MenuItem item)
        {
            this.MenuItems.Add(item);
            return this;
        }

        public OrderBuilder WithCreditCard()
        {
            this.PaymentTypeId = 2;
            return this;
        }

        public OrderBuilder WithDebitCard()
        {
            this.PaymentTypeId = 1;
            return this;
        }

        internal OrderCommand Build() 
            => new OrderCommand
            {
                Courses = MenuItems.Select(x => 
                    new Course
                    {
                        Dishes = new[] 
                        {
                            new Dish
                            {
                                Id = x.Id,
                                Ordered = true
                            }
                        }
                    }).ToArray(),
                PaymentTypeId = PaymentTypeId,
            };
    }
}
