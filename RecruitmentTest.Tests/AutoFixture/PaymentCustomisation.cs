using AutoFixture;
using NSubstitute;

namespace RecruitmentTest.Tests.AutoFixture
{
    internal class PaymentCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var paymentGateway = Substitute.For<PaymentGateway>();
                paymentGateway.Pay(Arg.Any<PaymentProvider>(), Arg.Any<int>(), Arg.Any<decimal>()).Returns(true);
                return paymentGateway;
            });
        }
    }
}
