using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RecruitmentTest.Controllers;
using RecruitmentTest.Tests.AutoFixture;
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
        public async Task Ordering_successfully_shows_thank_you_message(HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int debitCardPayment = 1;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new Features.Order { MenuItemId = orderItem.Id, PaymentTypeId = debitCardPayment });

            // Then
            result
                .Should().BeAssignableTo<ViewResult>()
                .Which.ViewData.Should().Contain("Message", "Thank you. Your order has been placed.");
        }

        [Theory, DomainAutoData]
        public async Task Ordering_successfully_shows_item_ordered_in_view(HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int creditCardPayment = 2;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new Features.Order { MenuItemId = orderItem.Id, PaymentTypeId = creditCardPayment });

            // Then
            result
                .Should().BeAssignableTo<ViewResult>()
                .Which.Model.Should().BeAssignableTo<Features.Order.Result>()
                .Which.Ordered.Should().BeEquivalentTo(orderItem);
        }

        [Theory, DomainAutoData]
        public async Task Ordering_with_debit_card_payment_through_PaymentGateway([Frozen] PaymentGateway paymentGateway, HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int debitCardPayment = 1;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new Features.Order { MenuItemId = orderItem.Id, PaymentTypeId = debitCardPayment });

            // Then
            paymentGateway.Received().Pay(Arg.Any<DebitCard>(), Arg.Any<int>(), Arg.Any<decimal>());
        }

        [Theory, DomainAutoData]
        public async Task Ordering_with_credit_card_payment_through_PaymentGateway([Frozen] PaymentGateway paymentGateway, HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int creditCardPayment = 2;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Order(new Features.Order { MenuItemId = orderItem.Id, PaymentTypeId = creditCardPayment });

            // Then
            paymentGateway.Received().Pay(Arg.Any<CreditCard>(), Arg.Any<int>(), Arg.Any<decimal>());
        }

        [Theory, DomainAutoData]
        public async Task Ordering_takes_correct_payment_amount([Frozen] PaymentGateway paymentGateway, HomeController sut, RestaurantDbContext context)
        {
            // Given
            await context.EnsureSeededAsync();
            var orderItem = context.MenuItems.First();

            // When
            sut.Order(new Features.Order { MenuItemId = orderItem.Id, PaymentTypeId = 1 });

            // Then
            paymentGateway.Received().Pay(Arg.Any<PaymentProvider>(), Arg.Any<int>(), orderItem.Price);
        }
    }
}
