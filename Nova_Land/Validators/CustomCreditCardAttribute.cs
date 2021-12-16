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

            if (payment != null)
            {
                if (payment.paymenttype == paymentMethod.Visa && payment.Number.Length != 16)
                    return new ValidationResult("Invalid card number, number of digits should be 16 for Visa card");
                else if (payment.paymenttype == paymentMethod.Visa && payment.Number.StartsWith('4'))
                    return new ValidationResult("Invalid card number, Visa card should begin with 4");
                else if (payment.paymenttype == paymentMethod.Master && payment.Number.Length != 16)
                    return new ValidationResult("Invalid card number, number of digits should be 16 for Master card");
                else if (payment.paymenttype == paymentMethod.Master && (int.Parse(payment.Number.Substring(0, 2)) < 51 || int.Parse(payment.Number.Substring(0, 2)) > 55))
                    return new ValidationResult("Invalid card number, first two digits of Master card should be between 51 to 55");
                else if (payment.paymenttype == paymentMethod.AmericanExpress && payment.Number.Length != 15)
                    return new ValidationResult("Invalid card number, number of digits should be 15 for American express card");
                else if (payment.paymenttype == paymentMethod.AmericanExpress && (int.Parse(payment.Number.Substring(0, 2)) < 34 || int.Parse(payment.Number.Substring(0, 2)) > 37))
                    return new ValidationResult("Invalid card number, first two digits should be between 34 and 37 for American express card");

            }
            else return new ValidationResult("Payment Information not found");

            return ValidationResult.Success;

        }
    }
}
