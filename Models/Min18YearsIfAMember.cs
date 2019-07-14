using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class Min18YearsIfAMember :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MemberShipTypeId == MemberShipType.unknown || customer.MemberShipTypeId == MemberShipType.payAsYouGo)
                return ValidationResult.Success;
            if (customer.birthdayDate == null)
                return new ValidationResult("Birthdate is required.");
            var age = DateTime.Today.Year - customer.birthdayDate.Value.Year;
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("customer should be at least 18 years old.");

        }
    }
}