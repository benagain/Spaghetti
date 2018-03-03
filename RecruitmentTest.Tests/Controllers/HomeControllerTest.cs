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
        public async Task Pay_with_debit_card_redirects_to_PaymentOk(HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int debitCardPayment = 1;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Update(new Features.Order { MenuItemId = orderItem.Id, PaymentType = debitCardPayment });

            // Then
            result
                .Should().BeAssignableTo<RedirectToActionResult>()
                .Which.ActionName.Should().Be(nameof(HomeController.PaymentOk));
        }

        [Theory, DomainAutoData]
        public async Task Pay_with_credit_card_redirects_to_PaymentOk(HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int creditCardPayment = 2;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Update(new Features.Order { MenuItemId = orderItem.Id, PaymentType = creditCardPayment });

            // Then
            result
                .Should().BeAssignableTo<RedirectToActionResult>()
                .Which.ActionName.Should().Be(nameof(HomeController.PaymentOk));
        }

        [Theory, DomainAutoData]
        public async Task Ordering_takes_payment_through_PaymentGateway([Frozen] PaymentGateway paymentGateway, HomeController sut, RestaurantDbContext setupDb)
        {
            // Given
            const int creditCardPayment = 2;
            await setupDb.EnsureSeededAsync();
            var orderItem = setupDb.MenuItems.First();

            // When
            var result = sut.Update(new Features.Order { MenuItemId = orderItem.Id, PaymentType = creditCardPayment });

            // Then
            paymentGateway.Received().Pay(Arg.Any<CreditCard>(), Arg.Any<int>(), Arg.Any<decimal>());
        }
    }
}
