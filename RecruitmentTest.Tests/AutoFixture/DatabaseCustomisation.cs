using AutoFixture;
using RecruitmentTest.Tests.Helpers;

namespace RecruitmentTest.Tests.AutoFixture
{
    public class DatabaseCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<InMemoryRestaurantDbContextBuilder>();
            fixture.Register(() => fixture.Create<InMemoryRestaurantDbContextBuilder>().Build());
        }
    }
}
