using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Account
    {
        private string name;
        private int funds;
        private ArrayList creditsTaken;

        public Account(string Name)
        {
            name = Name;
            creditsTaken = new ArrayList();
        }

        public void writeData()
        {
            Console.WriteLine(name + " has " + (funds) + " money, and " + summaryOfAllLoans() + " loans");
        }

        public int summaryOfAllLoans()
        {
            int summary = 0;
            foreach (Credit credit in creditsTaken)
            {
                summary += credit.HowMuchToPay();
            }
            return summary;
        }

        public void transfer(int addMoney)
        {
            funds += addMoney;
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
        }

        public void takeLoan(Credit newCredit)
        {
            creditsTaken.Add(newCredit);
            funds += newCredit.valueOfLoan;
        }
        public void pay(int valueToPay)
        {
            if (funds >= valueToPay)
            {
                funds -= valueToPay;
            }
        }
    }
}
