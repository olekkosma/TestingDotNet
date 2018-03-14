
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
        public string data { get; set; }

        public void addAccountsFromFile(string file)
        {
            data = System.IO.File.ReadAllText(file);
            string[] tokens = data.ToString().Split(
    new[] { "\r\n", "\r", "\n"," " },StringSplitOptions.None);
            var count = (tokens.Length + 1 / 2);

            for (int i = 0; i < count; i++)
            {
                accounts.Add(new Account(tokens[i], Int32.Parse(tokens[i + 1])));
                i++;
            }
        }
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
                        foreach (Account accountTmp in accounts)
            {
                if (name.Equals(accountTmp.name))
                {
                    return accountTmp;
                }
            }
            throw new NullReferenceException("There is not such an account");
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
				if (account.transfersToMake.Count > 0)
				{
					foreach (Transfer transfer in account.transfersToMake)
					{
                        try
                        {
                            logIn(transfer.to).transfer(transfer.Money);
                        }
                        catch ( NullReferenceException e)
                        {
                            logIn(transfer.from).transfer(transfer.Money);
                            throw new NullReferenceException("There is not such an account");
                        }
					}
				}
                account.nextWeek();
            }
        }
    }
}
