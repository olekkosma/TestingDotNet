using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Players.CodedUI
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class PlayerTest
    {
        public PlayerTest()
        {

        }

        //[TestMethod]
        public void addingNewPlayer()
        {
            this.UIMap.OpenBrowser();
            this.UIMap.OpenSite();
            this.UIMap.ClickPlayersNav();
            this.UIMap.ClickCreateNewPlayer();
            this.UIMap.TypeData();
            this.UIMap.ClickCreate();
            this.UIMap.CheckName();
            this.UIMap.ClickDeleteButtonMenu();
            this.UIMap.ConfirmDelete();
        }

        //[TestMethod]
        public void addingNotValidateData()
        {
            this.UIMap.OpenBrowser();
            this.UIMap.OpenSite();
            this.UIMap.ClickMatchesNav();
            this.UIMap.ClickCreateNewMatch();
            this.UIMap.ClickCreateFirstTime();
            this.UIMap.AssertIsValidationShowing();
        }

       // [TestMethod]
        public void addingNotValidateDataTwo()
        {
            this.UIMap.OpenBrowser();
            this.UIMap.OpenSite();
            this.UIMap.ClickMatchesNav();
            this.UIMap.ClickCreateNewMatch();
            this.UIMap.ClickCreateFirstTime();
            this.UIMap.TypeDataCityDate();
            this.UIMap.ClickCreateSecond();
            this.UIMap.AssesertMatchValidator();
        }

        [TestMethod]
        public void addingNotValidateDataThree()
        {
            this.UIMap.OpenBrowser();
            this.UIMap.OpenSite();
            this.UIMap.ClickMatchesNav();
            this.UIMap.ClickCreateNewMatch();
            this.UIMap.TypeDataCityDate();
            this.UIMap.WriteNonValidateMatchResult();
            this.UIMap.ClickCreateThird();
            this.UIMap.AssertMatchValidator();

        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
