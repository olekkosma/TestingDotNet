using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Bank
    {
        public ArrayList Accounts;

        public Bank()
        {
            Accounts = new ArrayList();
        }

        public void addNewAccount(Account account)
        {
            Accounts.Add(account);
        }
        public void nextWeek()
        {
            foreach(Account account in Accounts)
            {
                account.nextWeek();
            }
        }
    }
}
