namespace RecruitmentTest
{
    public interface PaymentGateway
    {
        bool Pay(PaymentProvider provider, int pin, decimal amount);
    }
}
