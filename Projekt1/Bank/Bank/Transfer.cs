
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	class Transfer : ITransfer
	{
		public string from { get; set; }
		public string to { get; set; }
		public int money { get; set; }

		public Transfer(string From,string To, int Money)
		{
			from = From;
			to = To;
			money = Money;
		}
    }
}
