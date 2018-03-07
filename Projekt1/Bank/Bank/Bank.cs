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
        public ArrayList accounts { get; set; }

        public Bank()
        {
            accounts = new ArrayList();
        }

        public void createAccount(string name,int funds)
        {
            bool isThere = isAlreadyThatAccount(name);

            if (!isThere)
            {
                accounts.Add(new Account(name,funds));
            }
        }

        public void createAccount(string name)
        {
            bool isThere = isAlreadyThatAccount(name);
            if (!isThere)
            {
                accounts.Add(new Account(name));
            }
        }

        public Account logIn(string name)
        {
            Account toReturn = null;
            foreach (Account accountTmp in accounts)
            {
                if (name.Equals(accountTmp.name))
                {
                    toReturn = accountTmp;
                }
            }
            return toReturn;
        }
        private bool isAlreadyThatAccount(string name)
        {
            bool isThere = false;
            foreach (Account accountTmp in accounts)
            {
                if (name.Equals(accountTmp.name))
                {
                    isThere = true;
                    throw new ArgumentException("Can't add two same name accounts");
                }
            }
            return isThere;
        }

        public void nextWeek()
        {
            foreach(Account account in accounts)
            {
                account.nextWeek();
            }
        }
    }
}
