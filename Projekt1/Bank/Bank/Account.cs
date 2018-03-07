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
        public string name { get; set; }
        public int funds { get; set; }
        public ArrayList creditsTaken { get; set; }

        public Account(string Name,int Funds)
        {
            name = Name;
            funds = Funds;
            creditsTaken = new ArrayList();
        }
        public Account(string newName)
        {
            name = newName;
            funds = 0;
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
