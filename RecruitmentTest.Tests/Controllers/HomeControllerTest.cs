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
    }
}
