namespace RecruitmentTest
{
    public class PaymentProviderFactory
    {
        public PaymentProvider Create(int paymentProviderId)
        {
            return
                paymentProviderId == 1 ? new DebitCard("0123 4567 8910 1112")
                : paymentProviderId == 2 ? new CreditCard("9999 9999 9999 9999")
                : (PaymentProvider)null;
        }
    }
}
