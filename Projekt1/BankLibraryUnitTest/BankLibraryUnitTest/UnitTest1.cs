using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankSystem;
namespace BankLibraryUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bank BGZ = new Bank();
            Account olek = new Account("olek");
            Account tomek = new Account("tomek");
            BGZ.addNewAccount(olek);
            BGZ.addNewAccount(tomek);
            Assert.AreEqual(2,BGZ.Accounts.Count);
        }
    }
}
