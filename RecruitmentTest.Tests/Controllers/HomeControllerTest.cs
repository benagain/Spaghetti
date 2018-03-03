using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
        public void Pay_with_debit_card_redirects_to_PaymentOk(HomeController sut)
        {
            // Given
            const int debitCardPayment = 1;

            // When
            var result = sut.Update(0, debitCardPayment);

            // Then
            result
                .Should().BeAssignableTo<RedirectToActionResult>()
                .Which.ActionName.Should().Be(nameof(HomeController.PaymentOk));
        }

        [Theory, DomainAutoData]
        public void Pay_with_credit_card_redirects_to_PaymentOk(HomeController sut)
        {
            // Given
            const int creditCardPayment = 2;

            // When
            var result = sut.Update(0, creditCardPayment);

            // Then
            result
                .Should().BeAssignableTo<RedirectToActionResult>()
                .Which.ActionName.Should().Be(nameof(HomeController.PaymentOk));
        }
    }
}
