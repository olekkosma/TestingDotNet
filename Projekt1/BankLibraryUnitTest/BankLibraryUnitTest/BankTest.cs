using System;
using BankSystem;
using BankSystem.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
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
        public void CatchingExceptionEveryAccountIsUnique()
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
        [TestMethod]
        public void PrintingDataAboutAccount()
        {
            Account randomPerson = new Account("adam", 500);
            string expected =  "adam has " + 500 + " money, and " + 0 + " loans";
            Assert.AreEqual(expected, randomPerson.writeData());
        }

        [TestMethod]
        public void TransferingMoney()
        {
            BGZ.logIn("tomek").transfer(10000);
            Assert.AreEqual(10000, BGZ.logIn("tomek").funds);
        }

        [TestMethod]
        public void PayingMoney()
        {
            BGZ.logIn("tomek").transfer(10000);
            BGZ.logIn("tomek").pay(8000);
            BGZ.logIn("tomek").pay(500);
            Assert.AreEqual(1500, BGZ.logIn("tomek").funds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantPayWhenEnoughMoney()
        {
            BGZ.logIn("tomek").transfer(10000);
            BGZ.logIn("tomek").pay(50000000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantPayForNegativeValue()
        {
            BGZ.logIn("tomek").transfer(10000);
            BGZ.logIn("tomek").pay(-500);
        }


        [TestMethod]
        public void TakingLoanAndPayingIt()
        {
            BGZ.logIn("tomek").transfer(10000);
            Credit tomekCredit = new Credit(500, 2, 100, 100);
            BGZ.logIn("tomek").takeLoan(tomekCredit);
            for (int i = 0; i < 2; i++) { BGZ.nextWeek(); }

            Assert.AreEqual(0, BGZ.logIn("tomek").creditsTaken.Count);
        }

        [TestMethod]
        public void TakingLoanThatWeCantAfford()
        {
            BGZ.logIn("tomek").transfer(50);
            Credit tomekCredit = new Credit(500, 2, 100, 100);
            BGZ.logIn("tomek").takeLoan(tomekCredit);
            for (int i = 0; i < 2; i++) { BGZ.nextWeek(); }

            Assert.AreEqual(0, BGZ.logIn("tomek").funds);
        }

        [TestMethod]
        public void ValuesOfLoansThatWeCantAfford()
        {
            BGZ.logIn("tomek").transfer(50);
            Credit tomekCredit = new Credit(500, 2, 150, 100);
            Credit tomekCreditSec = new Credit(500, 2, 150, 100);
            BGZ.logIn("tomek").takeLoan(tomekCredit);
            BGZ.logIn("tomek").takeLoan(tomekCreditSec);
            for (int i = 0; i < 2; i++) { BGZ.nextWeek(); }

            Assert.AreEqual(2, BGZ.logIn("tomek").creditsTaken.Count);
        }

        [TestMethod]
        public void PayingForPenaltyPerWeek()
        {
            BGZ.logIn("tomek").transfer(50);
            Credit tomekCredit = new Credit(500, 2, 150, 100);
            BGZ.logIn("tomek").takeLoan(tomekCredit);
            for (int i = 0; i < 5; i++) { BGZ.nextWeek(); }

            Assert.AreEqual(1000, tomekCredit.HowMuchToPay());
        }

        [TestMethod]
        public void NextWeekIsCounting()
        {
            Credit olekCredit = new Credit(1000, 6, 10, 100);
            BGZ.logIn("olek").takeLoan(olekCredit);
            for (int i = 0; i < 5; i++) { BGZ.nextWeek(); }

            Assert.AreEqual(true, olekCredit.IsTimeToPay());
        }

        [TestMethod]
        public void CountingValueToPayForOneCredit()
        {
            Credit olekCredit = new Credit(1000, 6, 10, 100);
            BGZ.logIn("olek").takeLoan(olekCredit);

            Assert.AreEqual(1100, olekCredit.HowMuchToPay());
        }

        [TestMethod]
        public void SummingValueOfAllLoansCombined()
        {
            Credit olekCredit = new Credit(1000, 6, 10, 100);
            Credit olekCreditSecond = new Credit(2000, 6, 10, 100);
            Credit olekCreditThird = new Credit(3000, 6, 10, 100);
            BGZ.logIn("olek").takeLoan(olekCredit);
            BGZ.logIn("olek").takeLoan(olekCreditSecond);
            BGZ.logIn("olek").takeLoan(olekCreditThird);

            Assert.AreEqual(6600, BGZ.logIn("olek").SummaryOfAllLoans());
        }

        [TestMethod]
        public void TransferingMoneyToOTherPersonCounting()
        {
            BGZ.createAccount("jendrzej");
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 300);
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 400);
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 500);

            Assert.AreEqual(3, BGZ.logIn("olek").transfersToMake.Count);
        }

        [TestMethod]
        public void TransferingMoneyToOTherPerson()
        {
            BGZ.logIn("olek").transfer(1000);
            BGZ.createAccount("jendrzej");
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 300);
            BGZ.nextWeek();
            Assert.AreEqual(300, BGZ.logIn("jendrzej").funds);
        }

        [TestMethod]
        public void TransferingMoneyToOtherPersonTwiceAtOnce()
        {
            BGZ.createAccount("jendrzej");
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 300);
            BGZ.logIn("olek").transferToAnotherAccount("jendrzej", 400);
            BGZ.nextWeek();
            Assert.AreEqual(700, BGZ.logIn("jendrzej").funds);
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

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV","|DataDirectory|\\data.csv",
            "data#csv",DataAccessMethod.Sequential),DeploymentItem("data.csv")]
        public void IsPayingWork()
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
        public void IsPayingWorksWhenNotEnoughMoney()
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
        public void ShimFileDoesNotExist()
        {
            using (ShimsContext.Create())
            {
                //Arrange
                System.IO.Fakes.ShimFile.ReadAllTextString = file => string.Format("olek 300 tomek 305");
                //Act
                Bank bank = new Bank();
                bank.addAccountsFromFile("nomeFile.extensions");
                var componentToTest = bank.accounts.Count;
                Assert.AreEqual(2, componentToTest);
            }

        }
    }
}
