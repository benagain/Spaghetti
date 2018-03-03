namespace RecruitmentTest
{
    public class PaymentGateway
    {
        public bool Pay(DebitCard debitCard, int pin, decimal amount)
        {
            return true;
        }

        internal bool Pay(CreditCard cc, int v1, decimal v2)
            => true;
    }
}
