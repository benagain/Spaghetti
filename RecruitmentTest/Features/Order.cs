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
            private readonly PaymentProviderFactory paymentProviders;
            private readonly PaymentGateway paymentGateway;

            public Handler(RestaurantDbContext context, PaymentProviderFactory paymentProviders, PaymentGateway paymentGateway)
            {
                this.context = context;
                this.paymentProviders = paymentProviders;
                this.paymentGateway = paymentGateway;
            }

            public Result Handle(OrderCommand order)
            {
                var items = FindOrderedItems(order);

                var price = items.Sum(x => x.Price);

                bool paidOk = TakePayment(order, price);

                return paidOk 
                    ? Result.Success(items.ToArray())
                    : Result.Failed();
            }

            private IEnumerable<MenuItem> FindOrderedItems(OrderCommand order) 
                => context.MenuItems.Where(x => order.MenuItemId.Contains(x.Id));

            private bool TakePayment(OrderCommand order, decimal price)
            {
                var paymentProvider = paymentProviders.Create(order.PaymentTypeId);

                return paymentProvider
                    .Map(provider => paymentGateway.Pay(provider, 1234, price))
                    .IfNone(false);
            }
        }
    }
}
