using Microsoft.VisualStudio.TestTools.UnitTesting;
using Players.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Tests.Models
{
    //[TestClass]
    public class MatchModelTest
    {
        [TestMethod]
        public void CreateCorrectMatch()
        {
            // Arrange
            var match = new Match
            {
                MatchID=3,
                City = "Wawrochy",
                Date= DateTime.Parse("2017-12-11"),
                Result="2:3"
            };
            var context = new ValidationContext(match, null, null);
            var result = new List<ValidationResult>();
            //Act
            var valid = Validator.TryValidateObject(match, context, result, true);

            // Assert
            Assert.IsTrue(valid);
        }
        [TestMethod]
        public void CreateInCorrectMatch_wrongMatchFormat()
        {
            // Arrange
            var match = new Match
            {
                MatchID = 3,
                City = "Wawrochy",
                Date = DateTime.Parse("2017-12-11"),
                Result = "2-3"
            };
            var context = new ValidationContext(match, null, null);
            var result = new List<ValidationResult>();
            //Act
            Validator.TryValidateObject(match, context, result, true);

            // Assert

            Assert.AreEqual(1, result.Count);

        }

        [TestMethod]
        public void CreateInCorrectMatch_wrongMatchFormatAndCity()
        {
            // Arrange
            var match = new Match
            {
                MatchID = 3,
                City = "Wawhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhrochy",
                Date = DateTime.Parse("2017-12-11"),
                Result = "2-3"
            };
            var context = new ValidationContext(match, null, null);
            var result = new List<ValidationResult>();
            //Act
            Validator.TryValidateObject(match, context, result, true);

            // Assert

            Assert.AreEqual(2, result.Count);

        }
        [TestMethod]
        public void CreateInCorrectMatch_wrongMatchFormat_CheckMessage()
        {
            // Arrange
            var match = new Match
            {
                MatchID = 3,
                City = "Wawhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhrochy",
                Date = DateTime.Parse("2017-12-11"),
                Result = "2:3"
            };
            var context = new ValidationContext(match, null, null);
            var result = new List<ValidationResult>();
            //Act
            var valid = Validator.TryValidateObject(match, context, result, true);

            // Assert
            Assert.IsFalse(valid);
        }
    }
}
