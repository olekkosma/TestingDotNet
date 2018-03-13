using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	public interface IAccount
	{
		
		void writeData();
		int SummaryOfAllLoans();
		void transfer(int money);
		void transferToAnotherAccount(int money, string nameToSend);
		void nextWeek();
		void takeLoan(ICredit newCredit);
		void pay(int valueToPay);
	}
}
