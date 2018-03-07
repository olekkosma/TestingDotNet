using System;
using BankSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLibraryUnitTest
{
    [TestClass]
    public class BankTest
    {
        Bank BGZ;

        [TestInitialize]
        public void Initialize() {
            BGZ = new Bank();
        }

        [TestMethod]
        public void AddingAccountsToBank()
        {
            BGZ.createAccount("olek");
            BGZ.createAccount("tomek");
            BGZ.createAccount("balcer");
            Assert.AreEqual(3,BGZ.accounts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CatchingExceptionWhenAddingAccounts()
        {
            BGZ.createAccount("olek");
            BGZ.createAccount("tomek");
            BGZ.createAccount("tomek");
        }

        [TestMethod]
        public void EveryAccountIsUnique()
        {
            BGZ.createAccount("olek");
            BGZ.createAccount("tomek");
            BGZ.createAccount("balcer");
            try
            {
                BGZ.createAccount("tomek");
                Assert.Fail();
            }
            catch (ArgumentException) { }
            CollectionAssert.AllItemsAreUnique(BGZ.accounts);
        }

        [TestMethod]
        public void IsEveryAccountPropperType()
        {
            BGZ.createAccount("olek");
            BGZ.createAccount("tomek");
            BGZ.createAccount("balcer");
            CollectionAssert.AllItemsAreInstancesOfType(BGZ.accounts,typeof(Account));
        }
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        
        
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV","|DataDirectory|\\data.csv",
            "data#csv",DataAccessMethod.Sequential),DeploymentItem("data.csv")]
        public void IsPayingWorks()
        {
            var name = TestContext.DataRow["name"].ToString();
            var funds = Int32.Parse(TestContext.DataRow["funds"].ToString());
            var payment1 = Int32.Parse(TestContext.DataRow["payment1"].ToString());
            var payment2 = Int32.Parse(TestContext.DataRow["payment2"].ToString());
            var payment3 = Int32.Parse(TestContext.DataRow["payment3"].ToString());
            var fundsAfterAll = Int32.Parse(TestContext.DataRow["fundsAfterAll"].ToString());
            BGZ.createAccount(name);
            BGZ.logIn(name).transfer(funds);
            BGZ.logIn(name).pay(payment1);
            BGZ.logIn(name).pay(payment2);
            BGZ.logIn(name).pay(payment3);
            Assert.AreEqual(fundsAfterAll, BGZ.logIn(name).funds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\data2.csv",
            "data2#csv", DataAccessMethod.Sequential), DeploymentItem("data2.csv")]
        public void IsPayingWorksWhenEnoughMoney()
        {
            var name = TestContext.DataRow["name"].ToString();
            var funds = Int32.Parse(TestContext.DataRow["funds"].ToString());
            var payment1 = Int32.Parse(TestContext.DataRow["payment1"].ToString());
            var payment2 = Int32.Parse(TestContext.DataRow["payment2"].ToString());
            var fundsAfterAll = Int32.Parse(TestContext.DataRow["fundsAfterAll"].ToString());
            BGZ.createAccount(name);
            BGZ.logIn(name).transfer(funds);
            BGZ.logIn(name).pay(payment1);
            BGZ.logIn(name).pay(payment2);
        }
    }
}
