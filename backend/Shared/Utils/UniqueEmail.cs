using backend.Data;
using System.ComponentModel.DataAnnotations;

namespace backend.Shared.Utils
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (UserDbContext)validationContext.GetService(typeof(UserDbContext))!;
            var email = value?.ToString();

            var exist = context.Accounts.FirstOrDefault(a => a.Email.Equals(email));

            if (exist != null)
            {
                return new ValidationResult("Email has been existed!");
            }

            return ValidationResult.Success;
        }
    }
}
