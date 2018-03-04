using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem;
namespace BankProjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank BGZ = new Bank();
            Account Olek = new Account("Olek");
            Account Tomek = new Account("Tomek");
            BGZ.addNewAccount(Olek);
            BGZ.addNewAccount(Tomek);
            
            Olek.takeLoan(new Credit(100000, 2, 50, 1000));

            Console.ReadKey();
        }
    }
}
