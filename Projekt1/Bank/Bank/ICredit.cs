using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	public interface ICredit
	{
		int ValueOfLoan { get; }
        int Property { get; set; }

        int HowMuchToPay();
		bool IsTimeToPay();
        bool PayCreditImmediately();
       
    }
}
