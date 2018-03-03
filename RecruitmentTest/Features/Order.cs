using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTest.Features
{
    public class OrderCommand
    {
        public int[] MenuItemId { get; set; }

        public int PaymentTypeId { get; set; }

        public class Result
        {
            public bool PaymentOk { get; private set; }

            public MenuItem[] Ordered { get; private set; }

            public static Result Success(MenuItem[] order) 
                => new Result
                {
                    PaymentOk = true,
                    Ordered = order,
                };

            public static Result Failed()
                => new Result { PaymentOk = false };
        }

        public class Handler
        {
            private readonly RestaurantDbContext context;
            private readonly PaymentGateway paymentGateway;

            public Handler(RestaurantDbContext context, PaymentGateway paymentGateway)
            {
                this.context = context;
                this.paymentGateway = paymentGateway;
            }

            public Result Handle(OrderCommand order)
            {
                var items = FindOrderedItems(order);

                var price = items.Sum(x => x.Price);

                bool paid = TakePayment(order, price);

                return paid 
                    ? Result.Success(items.ToArray())
                    : Result.Failed();
            }

            private IEnumerable<MenuItem> FindOrderedItems(OrderCommand order)
                => order.MenuItemId.Join(
                    context.MenuItems,
                    id => id,
                    item => item.Id,
                    (_, item) => item);

            private bool TakePayment(OrderCommand order, decimal price)
            {
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

                bool paid = false;
                if (paymentProvider != null)
                {
                    paid = paymentGateway.Pay(paymentProvider, 1234, price);

                }

                return paid;
            }
        }
    }
}
