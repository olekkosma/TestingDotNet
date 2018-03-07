using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Credit
    {
       
        public int valueOfLoan { get; set; }
        public int loanTimeInWeeks { get; set; }
        public int provisionInProcents { get; set; }
        public int penaltyPerWeek { get; set; }
        public int totalValueToPay { get; set; }

        public Credit(int ValueOfLoan, int LoanTimeInWeeks, int ProvisionInProcents, int PenaltyPerWeek)
        {
            valueOfLoan = ValueOfLoan;
            loanTimeInWeeks = LoanTimeInWeeks;
            provisionInProcents = ProvisionInProcents;
            penaltyPerWeek = PenaltyPerWeek;
            totalValueToPay = valueOfLoan + (provisionInProcents *valueOfLoan /100);
        }

        public int HowMuchToPay()
        {
            return totalValueToPay;
        }


        public bool IsTimeToPay()
        {
            loanTimeInWeeks--;
            if (loanTimeInWeeks <= 0)
            {
                if (loanTimeInWeeks < 0)
                {
                    totalValueToPay += penaltyPerWeek;
                }
                return true;
            }
            return false;
        }
    }
}
