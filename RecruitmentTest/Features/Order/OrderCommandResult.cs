namespace RecruitmentTest.Features
{
    public class OrderCommandResult
    {
        public bool PaymentOk { get; private set; }

        public MenuItem[] Ordered { get; private set; }

        public static OrderCommandResult Success(MenuItem[] order)
            => new OrderCommandResult
            {
                PaymentOk = true,
                Ordered = order,
            };

        public static OrderCommandResult Failed()
            => new OrderCommandResult { PaymentOk = false };
    }
}
