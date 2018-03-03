using AutoFixture;
using AutoFixture.Xunit2;

namespace RecruitmentTest.Tests.AutoFixture
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class DomainAutoDataAttribute : AutoDataAttribute
    {
        public DomainAutoDataAttribute()
            : base(() => new Fixture()
                .Customize(new DomainAutoDataCustomisations()))
        {
        }
    }
}
