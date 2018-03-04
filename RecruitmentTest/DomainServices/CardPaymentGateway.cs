namespace RecruitmentTest
{
    public class CardPaymentGateway : PaymentGateway
    {
        public bool Pay(PaymentProvider provider, int pin, decimal amount) => true;
    }
}
