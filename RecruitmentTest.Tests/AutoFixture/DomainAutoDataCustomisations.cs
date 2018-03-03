using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace RecruitmentTest.Tests.AutoFixture
{
    public class DomainAutoDataCustomisations : CompositeCustomization
    {
        public DomainAutoDataCustomisations()
            : base(
                  new AutoConfiguredNSubstituteCustomization(),
                  new DatabaseCustomisation())
        {
        }
    }
}
