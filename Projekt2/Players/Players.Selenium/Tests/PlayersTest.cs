using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Selenium.Tests
{
    //[TestClass]
    public class PlayersTest
    {
        [TestMethod]
        public void StringTextOnMainSite()
        {
            IWebDriver driver = new InternetExplorerDriver();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/");
            IWebElement webElement = driver.FindElement(By.Id("github"));
            StringAssert.Contains(webElement.Text, "Checkout my Github");
        }

        [TestMethod]
        public void searchTableName()
        {
            IWebDriver driver = new InternetExplorerDriver();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("matchId"));
            StringAssert.Contains(webElement.Text, "Matches");
        }

        [TestMethod]
        public void searchStringKeysSend()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("SearchString"));
            IWebElement webElement2 = driver.FindElement(By.Id("buttonSearch"));
            webElement.SendKeys("Bar");
            IWebElement webElement3 = driver.FindElement(By.Name("Barcelona"));
            StringAssert.Contains(webElement3.Text, "Barcelona");
        }
        [TestMethod]
        public void swichingBetweenTables()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("statisticNav"));
            webElement.Click();
            IWebElement webElement2 = driver.FindElement(By.Id("statisticId"));
            StringAssert.Contains(webElement2.Text, "Statistics");
        }

        [TestMethod]
        public void switchingToAddMatch()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("addMatch"));
            webElement.Click();
            string url = driver.Url;
            StringAssert.Contains(url, "http://localhost:59511/Match/Create");
        }

       [TestMethod]
        public void AddingMatch()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("addMatch"));
            webElement.Click();
            webElement = driver.FindElement(By.Id("City"));
            webElement.SendKeys("Praga");
            webElement = driver.FindElement(By.Id("Date"));
            webElement.SendKeys("11/11/2016");
            webElement = driver.FindElement(By.Id("Result"));
            webElement.SendKeys("3:2");
            webElement = driver.FindElement(By.Id("button"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            webElement.Click();
            string url = driver.Url;
            StringAssert.Contains(url, "http://localhost:59511/Match");
        }

        [TestMethod]
        public void deletingMatch()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("Praga"));
            webElement.Click();
            webElement = driver.FindElement(By.Id("button"));
            webElement.Click();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);
            string url = driver.Url;
            StringAssert.Contains(url, "http://localhost:59511/Match");
        }

        [TestMethod]
        public void creatingMatchWrongData()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:59511/Match");
            IWebElement webElement = driver.FindElement(By.Id("addMatch"));
            webElement.Click();
            webElement = driver.FindElement(By.Id("City"));
            webElement.SendKeys("Praga");
            webElement = driver.FindElement(By.Id("Date"));
            webElement.SendKeys("11/11/2016");
            webElement = driver.FindElement(By.Id("Result"));
            webElement.SendKeys("3-2");
            webElement = driver.FindElement(By.Id("button"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            webElement.Click();
            webElement = driver.FindElement(By.Id("ResultError"));
            StringAssert.Contains(webElement.Text, "Match result format should be");
        }
    }
}
