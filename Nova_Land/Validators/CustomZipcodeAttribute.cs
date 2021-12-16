using Nova_Land.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Nova_Land.Validators
{
    public class CustomZipcodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            var payment = (Payment)validationContext.ObjectInstance;
            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\s(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            if (payment.Country.ToLower() == "usa")
            {
                if (!Regex.Match(payment.Zip, _usZipRegEx).Success)
                    return new ValidationResult("Invalid Zip Code for USA");
            }
            else if (payment.Country.ToLower() == "canada")
            {
                if (!Regex.Match(payment.Zip, _caZipRegEx).Success)
                    return new ValidationResult("Invalid Zip Code for Canada");
            }
            return ValidationResult.Success;
        }



    }
}
