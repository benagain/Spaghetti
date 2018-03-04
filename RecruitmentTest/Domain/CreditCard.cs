namespace RecruitmentTest
{
    public class CreditCard : PaymentProvider
    {
        public CreditCard(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public string CardNumber { get; }
    }
}
