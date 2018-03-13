using System;
using BankSystem;
using BankSystem.Fakes;
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
			BGZ.createAccount("olek",10000);
			BGZ.createAccount("tomek");
			BGZ.createAccount("balcer");
		}

		[TestMethod]
		public void StubPayCreditBeforeTime()
		{
			// Arrange:  
			ICredit credit = new StubICredit()
			 {
                PayCreditImmediately = () => { return true; }
			 };
            BGZ.logIn("olek").takeLoan(credit);
            // Act:  
            bool actualValue = BGZ.logIn("olek").payCreditBeforeTime(credit);
			// Assert:  
			Assert.AreEqual(0, BGZ.logIn("olek").creditsTaken.Count);
		}

        [TestMethod]
        public void AddingAccountsToBank()
        {
			BGZ.createAccount("jendrzej");
            Assert.AreEqual(4,BGZ.accounts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CatchingExceptionWhenAddingAccounts()
        {
			BGZ.createAccount("jendrzej");
			BGZ.createAccount("jendrzej");
        }

        [TestMethod]
        public void EveryAccountIsUnique()
        {
            BGZ.createAccount("jendrzej");
            try
            {
                BGZ.createAccount("jendrzej");
                Assert.Fail();
            }
            catch (ArgumentException) { }
            CollectionAssert.AllItemsAreUnique(BGZ.accounts);
        }

        [TestMethod]
        public void IsEveryAccountPropperType()
        {
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
			Bank mBank = new Bank();
            var name = TestContext.DataRow["name"].ToString();
            var funds = Int32.Parse(TestContext.DataRow["funds"].ToString());
            var payment1 = Int32.Parse(TestContext.DataRow["payment1"].ToString());
            var payment2 = Int32.Parse(TestContext.DataRow["payment2"].ToString());
            var payment3 = Int32.Parse(TestContext.DataRow["payment3"].ToString());
            var fundsAfterAll = Int32.Parse(TestContext.DataRow["fundsAfterAll"].ToString());
			mBank.createAccount(name);
			mBank.logIn(name).transfer(funds);
			mBank.logIn(name).pay(payment1);
			mBank.logIn(name).pay(payment2);
			mBank.logIn(name).pay(payment3);
            Assert.AreEqual(fundsAfterAll, mBank.logIn(name).funds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\data2.csv",
            "data2#csv", DataAccessMethod.Sequential), DeploymentItem("data2.csv")]
        public void IsPayingWorksWhenEnoughMoney()
        {
			Bank mBank = new Bank();
			var name = TestContext.DataRow["name"].ToString();
            var funds = Int32.Parse(TestContext.DataRow["funds"].ToString());
            var payment1 = Int32.Parse(TestContext.DataRow["payment1"].ToString());
            var payment2 = Int32.Parse(TestContext.DataRow["payment2"].ToString());
            var fundsAfterAll = Int32.Parse(TestContext.DataRow["fundsAfterAll"].ToString());
			mBank.createAccount(name);
			mBank.logIn(name).transfer(funds);
			mBank.logIn(name).pay(payment1);
			mBank.logIn(name).pay(payment2);
        }

		[TestMethod]
		public void transferingMoney()
		{
			BGZ.logIn("olek").transfer(1000);
			BGZ.createAccount("jendrzej");
			BGZ.logIn("olek").transferToAnotherAccount(300, "jendrzej");
			BGZ.nextWeek();
			Assert.AreEqual(300, BGZ.logIn("jendrzej").funds);
		}

	}
}
