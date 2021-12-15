using Nova_Land.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Validators
{
    public class CustomCreditCardAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var payment = (Payment)validationContext.ObjectInstance;

            return ValidationResult.Success;

        }
    }
}
