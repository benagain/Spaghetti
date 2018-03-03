using System;
using System.Linq;

namespace RecruitmentTest.Features
{
    public class Order
    {
        public int MenuItemId { get; set; }

        public int PaymentTypeId { get; set; }

        public class CommandHandler
        {
            private readonly RestaurantDbContext context;
            private readonly PaymentGateway paymentGateway;

            public CommandHandler(RestaurantDbContext context, PaymentGateway paymentGateway)
            {
                this.context = context;
                this.paymentGateway = paymentGateway;
            }

            public bool Handle(Order order)
            {
                var item = context.MenuItems
                    .SingleOrDefault(x => x.Id == order.MenuItemId)
                    ?? throw new ArgumentException($"Menu item ${order.MenuItemId} not found");

                PaymentProvider paymentProvider = null;

                switch (order.PaymentTypeId)
                {
                    case 1:
                        paymentProvider = new DebitCard("0123 4567 8910 1112");
                        break;

                    case 2:
                        paymentProvider = new CreditCard("9999 9999 9999 9999");
                        break;
                }

                if (paymentProvider != null)
                {
                    var paid = paymentGateway.Pay(paymentProvider, 1234, item.Price);

                    if (paid) return true;
                }

                return false;
            }
        }
    }
}
