
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
	public class Transfer : ITransfer
	{
        public string from;
        public string to;
        private int money;
        public int Money
        {
            get { return money; }
        }
        public Transfer(string From,string To, int Money)
		{
			from = From;
			to = To;
			money = Money;
		}
    }
}
