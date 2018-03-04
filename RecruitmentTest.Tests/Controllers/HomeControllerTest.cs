using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RecruitmentTest.Controllers;
using RecruitmentTest.Features;
using RecruitmentTest.Tests.AutoFixture;
using RecruitmentTest.Tests.Helpers;
using Xunit;

namespace RecruitmentTest.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Theory, DomainAutoData]
        public void Index(HomeController sut)
        {
            // Act
            var result = sut.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory, DomainAutoData]
        public async Task Index_returns_all_menu_items_grouped_by_course(HomeController sut, RestaurantDbContext setupDb, MenuItemType[] courses)
        {
            // Given
            await setupDb.MenuItemTypes.AddRangeAsync(courses);
            await setupDb.SaveChangesAsync();

            // When
            var result = sut.Index();

            var model = result
                .Should().BeAssignableTo<ViewResult>()
                .Which.Model.Should().BeAssignableTo<Features.Menu>()
                .Which.Courses.Should().BeEquivalentTo(courses);
        }

        [Theory, DomainAutoData]
        public void About(HomeController sut)
        {
            // Act
            var result = sut.About() as ViewResult;

            // Assert
            Assert.Equal("Davey P's Italian Restauranty was established in 2016 to produce Spaghetti Code.", result.ViewData["Message"]);
        }

        [Theory, DomainAutoData]
        public void Contact(HomeController sut)
        {
            // Act
            var result = sut.Contact() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory, DomainAutoData]
        public async Task Ordering_successfully_shows_item_ordered_in_view(TestableHomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new OrderBuilder(orderItem).Build());

            // Then
            result
                .Should().BeAssignableTo<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderCommandResult>()
                .Which.Ordered.Should().BeEquivalentTo(orderItem);
        }

        [Theory, DomainAutoData]
        public async Task Ordering_with_debit_card_payment_through_PaymentGateway([Frozen] PaymentGateway paymentGateway, TestableHomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new OrderBuilder(orderItem).WithDebitCard().Build());

            // Then
            paymentGateway.Received().Pay(Arg.Any<DebitCard>(), Arg.Any<int>(), Arg.Any<decimal>());
        }

        [Theory, DomainAutoData]
        public async Task Ordering_with_credit_card_payment_through_PaymentGateway([Frozen] PaymentGateway paymentGateway, TestableHomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new OrderBuilder(orderItem).WithCreditCard().Build());

            // Then
            paymentGateway.Received().Pay(Arg.Any<CreditCard>(), Arg.Any<int>(), Arg.Any<decimal>());
        }

        [Theory, DomainAutoData]
        public async Task Ordering_one_item_takes_correct_payment_amount([Frozen] PaymentGateway paymentGateway, TestableHomeController sut, RestaurantDbContext context)
        {
            // Given
            await context.EnsureSeededAsync();
            var orderItem = context.MenuItems.First();

            // When
            var result = sut.Order(new OrderBuilder(orderItem).Build());

            // Then
            paymentGateway.Received().Pay(Arg.Any<PaymentProvider>(), Arg.Any<int>(), orderItem.Price);
        }

        [Theory, DomainAutoData]
        public async Task Ordering_multiple_items_takes_correct_payment_amount([Frozen] PaymentGateway paymentGateway, TestableHomeController sut, RestaurantDbContext context)
        {
            // Given
            await context.EnsureSeededAsync();
            var orderItems = context.MenuItems.Take(3).ToList();
            var totalPrice = orderItems.Aggregate(0m, (sum, item) => sum + item.Price);

            // When
            var result = sut.Order(new OrderBuilder(orderItems).Build());

            // Then
            paymentGateway.Received().Pay(Arg.Any<PaymentProvider>(), Arg.Any<int>(), totalPrice);
        }

        public class TestableHomeController : HomeController
        {
            private readonly Func<OrderCommandHandler> orderHandlerFactory;

            public TestableHomeController(RestaurantDbContext context, Func<OrderCommandHandler> orderHandlerFactory)
                : base(context)
            {
                this.orderHandlerFactory = orderHandlerFactory;
            }

            public ActionResult Order(OrderCommand order)
                => base.Order(order, orderHandlerFactory());
        }
    }
}
