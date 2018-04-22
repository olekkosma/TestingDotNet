using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Players;
using Players.Controllers;
using Players.Models;
using Players.Repositories.Interfaces;
namespace Players.Tests.Controllers
{
   // [TestClass]
    public class MatchControllerTest
    {
        Players.Models.Match match1;
        Players.Models.Match match2;
        string CityName;
        [TestInitialize]
        public void Initialize()
        {
            CityName = "Barcelona";
            match1 = new Players.Models.Match()
            {
                City = CityName
            };
            match2 = new Players.Models.Match()
            {
                City = CityName + "b"
             };
        }
        [TestMethod]
        public void IsReturningEmptyList()
        {
            // Arrange
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(new List<Players.Models.Match>());
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(null,null) as ViewResult;
            var model =(List<Players.Models.Match>) result.Model;
            // Assert
            Assert.AreEqual(0,model.Count);
        }

        [TestMethod]
        public void IsReturningNameOfMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(null, null) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(CityName, model.First().City);
        }

        [TestMethod]
        public void IsReturningNumberOfMatches()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1, match2 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(null, null) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void IsReturningSearchedMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1, match2 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            ViewResult result = controller.Index(null, CityName+"b") as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(1, model.Count);
        }
        [TestMethod]
        public void IsReturningIndescending()
        {
            string sortingOrder = "city_desc";
            // Arrange
            var list = new List<Players.Models.Match> { match1, match2 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(sortingOrder, CityName) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(CityName + "b", model[0].City);
        }

        [TestMethod]
        public void IsReturningIndescendingDate()
        {
            string sortingOrder = "date_desc";
            // Arrange
            var list = new List<Players.Models.Match> {match1, match2};
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(sortingOrder, CityName) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(CityName, model[0].City);
        }

        [TestMethod]
        public void IsReturningAscendingDate()
        {
            string sortingOrder = "Date";
            // Arrange
            var list = new List<Players.Models.Match> {match2, match1};
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(sortingOrder, CityName) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(CityName, model[1].City);
        }

        [TestMethod]
        public void IsReturningSearchedCities()
        {
            string sortingOrder = "date_desc";
            // Arrange
            var list = new List<Players.Models.Match> { match1, match2 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Index(sortingOrder, CityName) as ViewResult;
            var model = (List<Players.Models.Match>)result.Model;
            // Assert
            Assert.AreEqual(2, model.Count);
        }
        [TestMethod]
        public void IsReturningDetailsMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1, match2 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            mockedService.Setup(m => m.FindById(2)).Returns(match2);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Details(2) as ViewResult;
            var model = (Players.Models.Match)result.Model;
            // Assert
            Assert.AreEqual(match2, model);
        }

        [TestMethod]
        public void IsReturningNotFoundDetails()
        {
            string expected = "System.Web.Mvc.HttpNotFoundResult";
            // Arrange
            var list = new List<Players.Models.Match> { match1};
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ActionResult result = controller.Details(1555);
            // Assert
            Assert.AreEqual(expected, result.ToString());
        }

        [TestMethod]
        public void IsReturningNotFoundEdit()
        {
            string expected = "System.Web.Mvc.HttpNotFoundResult";
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ActionResult result = controller.Edit(1555);
            // Assert
            Assert.AreEqual(expected, result.ToString());
        }

        [TestMethod]
        public void AfterCreateReturnToIndex()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1};
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            var result =  controller.Create(match1);
            var redirectResult = (ActionResult)result;
            // Assert
            Assert.IsInstanceOfType(redirectResult, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ReturnEditedMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Edit(1) as ViewResult;
            var model = (Players.Models.Match)result.Model;
            // Assert
            Assert.AreEqual(match1,model);
        }

        [TestMethod]
        public void GetReturnedEditedMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            ViewResult result = controller.Edit(match2) as ViewResult;
            var redirectResult = (ActionResult)result;
            // Assert
            Assert.IsInstanceOfType(redirectResult, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void CreateMatchWithWrongData()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(0)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            controller.ModelState.AddModelError("Error", "error");
            // Act
            var result = controller.Create(match2);
            // Assert
            var model = ((ViewResult)result).Model as Players.Models.Match;
            Assert.AreEqual(match2, model);

        }
        [TestMethod]
        public void EditMatchWithNewWrongData()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(0)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            controller.ModelState.AddModelError("Error", "error");
            // Act
            var result = controller.Edit(match2);
            // Assert
            var model = ((ViewResult)result).Model as Players.Models.Match;
            Assert.AreEqual(match2, model);
           
        }

        [TestMethod]
        public void DeleteExistingMatch()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            controller.ModelState.AddModelError("Error", "error");
            // Act
            var result = controller.Delete(1);
            // Assert
            var model = ((ViewResult)result).Model as Players.Models.Match;
            Assert.AreEqual(match1, model);

        }

        [TestMethod]
        public void DeleteNotExistingMatchRedirect()
        {
            string expected = "System.Web.Mvc.HttpNotFoundResult";
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            controller.ModelState.AddModelError("Error", "error");
            // Act
            var result = controller.Delete(12345);
            // Assert
            Assert.AreEqual(expected, result.ToString());

        }

        [TestMethod]
        public void DeleteExistingMatchDeleteConfirmed()
        {
            // Arrange
            var list = new List<Players.Models.Match> { match1 };
            var mockedService = new Mock<IMatchRepository>();
            mockedService.Setup(m => m.GetAllMatches()).Returns(list);
            mockedService.Setup(m => m.FindById(1)).Returns(match1);
            MatchController controller = new MatchController(mockedService.Object);
            // Act
            var result = controller.DeleteConfirmed(5461);
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
