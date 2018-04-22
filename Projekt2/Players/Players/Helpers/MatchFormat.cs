using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Players.Helpers
{
    public class MatchFormat : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            var minutes = value.ToString();
            if (Regex.IsMatch(minutes, "^[0-9]{1,2}[:][0-9]{1,2}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Match result format should be like 1:1");
        }
    }
}