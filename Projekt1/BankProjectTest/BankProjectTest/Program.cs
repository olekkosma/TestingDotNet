using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BankSystem;
namespace BankProjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.addAccountsFromFile("accounts.txt");
            
            foreach (Account item in bank.accounts)
            {
                item.writeData();
            }
            
            Console.ReadKey();
        }
    }
}
