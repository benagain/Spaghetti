using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTest.Features
{
    public class OrderCommandHandler
    {
        private readonly RestaurantDbContext context;
        private readonly PaymentProviderFactory paymentProviders;
        private readonly PaymentGateway paymentGateway;

        public OrderCommandHandler(RestaurantDbContext context, PaymentProviderFactory paymentProviders, PaymentGateway paymentGateway)
        {
            this.context = context;
            this.paymentProviders = paymentProviders;
            this.paymentGateway = paymentGateway;
        }

        public OrderCommandResult Handle(OrderCommand order)
        {
            var items = FindOrderedItems(order);

            var price = items.Sum(x => x.Price);

            bool paidOk = TakePayment(order, price);

            return paidOk
                ? OrderCommandResult.Success(items.ToArray())
                : OrderCommandResult.Failed();
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
