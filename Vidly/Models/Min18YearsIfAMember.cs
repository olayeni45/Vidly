using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer cst = (Customer)validationContext.ObjectInstance;

            if (cst.MembershipTypeId == MembershipType.Unknown ||
                cst.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (cst.Birthdate == null)
                return new ValidationResult("Birthdate is required.");

            int age = DateTime.Now.Year - cst.Birthdate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}