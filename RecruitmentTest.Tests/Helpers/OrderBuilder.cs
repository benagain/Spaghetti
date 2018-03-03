using RecruitmentTest.Features;

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



        public OrderBuilder WithItem(MenuItem item)
        {
            this.MenuItemId = item.Id;
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

        internal Order Build() 
            => new Order
            {
                MenuItemId = MenuItemId,
                PaymentTypeId = PaymentTypeId,
            };
    }
}
