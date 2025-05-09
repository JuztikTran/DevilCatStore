using StoreService.Data;
using System.ComponentModel.DataAnnotations;

namespace StoreService.Shared
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var context = (UserDBContext)validationContext.GetService(typeof(UserDBContext))!;
            var email = value as string;

            if (context.Accounts.Any(a => a.Email.Equals(email)))
            {
                return new ValidationResult("Email already exist.");
            }


            return ValidationResult.Success!;
        }
    }
}
