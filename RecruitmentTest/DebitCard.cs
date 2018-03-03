using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTest
{
    public class DebitCard
    {
        public DebitCard(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public string CardNumber { get; set; }
    }
}
