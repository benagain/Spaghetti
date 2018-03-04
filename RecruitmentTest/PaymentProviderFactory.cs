using LanguageExt;
using static LanguageExt.Prelude;

namespace RecruitmentTest
{
    public class PaymentProviderFactory
    {
        public Option<PaymentProvider> Create(int paymentProviderId)
        {
            if (paymentProviderId == 1) return new DebitCard("0123 4567 8910 1112");
            if (paymentProviderId == 2) return new CreditCard("9999 9999 9999 9999");
            return None;
        }
    }
}
