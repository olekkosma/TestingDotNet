

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Account : IAccount
    {
        public string name { get; set; }
        public int funds { get; set; }
        public ArrayList creditsTaken { get; set; }
		public ArrayList transfersToMake { get; set; }

		public Account(string Name,int Funds)
        {
            name = Name;
            funds = Funds;
            creditsTaken = new ArrayList();
			transfersToMake = new ArrayList();
		}
		public Account(string newName) : this(newName, 0) { }

        public string writeData()
        {
            return (name + " has " + (funds) + " money");
        }

        public string writeDataExtended()
        {
            return (writeData()+", and " + SummaryOfAllLoans() + " loans");
        }


        public int SummaryOfAllLoans()
        {
            int summary = 0;
            foreach (ICredit credit in creditsTaken)
            {
                summary += credit.HowMuchToPay();
            }
            return summary;
        }

        public void transfer(int money)
        {
			if (money > 0)
			{
				funds += money;
			}
        }
		public void transferToAnotherAccount(string to, int money)
		{
			if (money > 0 && funds> money)
			{
				funds -= money;
				transfersToMake.Add(new Transfer(name,to,money));
			}
		}

        public bool payCreditBeforeTime(ICredit credit)
        {
            bool isPaid = credit.PayCreditImmediately();
            if (isPaid)
            {
                creditsTaken.Remove(credit);
            }
            return isPaid;
        }

        public void nextWeek()
        {
            ArrayList creditsToDelete = new ArrayList(); 

            foreach (Credit credit in creditsTaken)
            {
                if (credit.IsTimeToPay())
                {
                    int howMuchToPay = credit.HowMuchToPay();
                    if (funds >= howMuchToPay)
                    {
                        funds -= howMuchToPay;
                        creditsToDelete.Add(credit);
                    }else
                    {
                        credit.totalValueToPay =howMuchToPay - funds;
                        funds = 0;
                    }
                }
            }
            foreach (Credit credit in creditsToDelete)
                creditsTaken.Remove(credit);

			transfersToMake.Clear();

		}

        public void takeLoan(ICredit newCredit)
        {
            creditsTaken.Add(newCredit);
            funds += newCredit.ValueOfLoan;
        }
        public void pay(int valueToPay)
        {
            if (valueToPay >= 0)
            {
                if (funds >= valueToPay)
                {
					funds -= valueToPay;
				}
                else
                {
                    throw new ArgumentException("Not enough money");
                }
            }else
            {
                throw new ArgumentException("value to pay has to be positive");
            }
        }
    }
}
