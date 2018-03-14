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
                Console.WriteLine(item.writeDataExtended());
            }
            
            bank.logIn("olek").pay(50);

            Console.WriteLine("olek paid 50 zl for bread");
            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("olek wants to buy a house");
            Console.WriteLine("olek is taking a huuge loan");

            bank.logIn("olek").takeLoan(new Credit(500000, 20, 20, 1000));

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("olek have to pay for loan after 20 weeks...lot of time");
            Console.WriteLine("in meantime he is buying house");

            bank.logIn("olek").pay(500000);

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("after 20 weeks");

            for(int i = 0; i < 20; i++) { bank.nextWeek(); }

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("he paid only 250 zl of his last credit");
            Console.WriteLine("after 5 weeks");

            for (int i = 0; i < 5; i++) { bank.nextWeek(); }

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("is getting worse... olek has to pay even for penalty 1000 per week!");
            Console.WriteLine("olek begged his friend to borrow some money");

            bank.logIn("fred").transferToAnotherAccount("olek", 40000);
            bank.nextWeek();

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("but after paying time again he has nothing...");

            bank.nextWeek();

            Console.WriteLine(bank.logIn("olek").writeDataExtended());
            Console.WriteLine("and so on, so on...life is brutal");
            Console.ReadKey();
        }
    }
}
