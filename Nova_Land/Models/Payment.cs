using Nova_Land.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public enum paymentMethod
    {
        Master,
        Visa,
        AmericanExpress
    }
    public class Payment
    {
        internal string zip;

        public int Id { get; set; }
        [Required]
        public paymentMethod paymenttype { get; set; }
       
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+)|([A-za-z]+[\s]{1}[A-za-z]+[\s]{1}[A-Za-z]+))$",
                            ErrorMessage = "Please enter a valid Name")]
        public string CreditCardHolderName { get; set; }
        
        [Required]
        ///[StringLength(16,ErrorMessage = "Please enter only 16 digits")]
        ///[RegularExpression(@"^([0-9]{16})$",
        ///                    ErrorMessage = "Please enter a valid Credit Card number")]
        [CustomCreditCard]
        public string Number  { get; set; }
        [Required]
        [StringLength(3)]

        [RegularExpression(@"^([0-9]{3})$",
                            ErrorMessage = "Please enter a valid CVV")]
        public string cvv { get; set; }
        [Required]
        [StringLength(7)]
        [RegularExpression(@"^((0[1-9]{1}[\/]{1}(201[6-9]{1}|202[0-9]{1}|203[0-1]{1}))|(1[0-2]{1}[\/]{1}(201[6-9]{1}|202[0-9]{1}|203[0-1]{1})))$", ErrorMessage = "Please enter a valid Expiry Date")]
        public string Exp_dt { get; set; }

        [RegularExpression(@"^([0-9]+[\s]+[A-Za-z]+)$",
                            ErrorMessage = "Please enter a valid Street address")]
        public string St_Address { get; set; }

        [RegularExpression(@"^([A-Za-z]+)$",
                            ErrorMessage = "Please enter a valid City")]
        public string City { get; set; }

        [RegularExpression(@"^([A-Za-z]+|([A-Za-z]+[\s]{1}[A-Za-z]+))$",
                           ErrorMessage = "Please enter a valid State")]
        public string State { get; set; }

        [RegularExpression(@"^([Uu]{1}[Ss]{1}[Aa]{1}|[Cc]{1}[Aa]{1}[Nn]{1}[Aa]{1}[Dd]{1}[Aa]{1})$",
                           ErrorMessage = "Please enter a valid Country")]
        public string Country { get; set; }

        [CustomZipcode]
        public string Zip { get; set; }
    }
}
