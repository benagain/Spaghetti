using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTest
{
    public class PaymentGateway
    {
        public bool Pay(DebitCard debitCard, int pin, decimal amount)
        {
            return true;
        }
    }
}
