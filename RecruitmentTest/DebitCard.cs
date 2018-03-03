namespace RecruitmentTest
{
    public class DebitCard : PaymentProvider
    {
        public DebitCard(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public string CardNumber { get; set; }
    }
}
