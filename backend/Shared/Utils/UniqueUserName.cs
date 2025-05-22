using backend.Data;
using System.ComponentModel.DataAnnotations;

namespace backend.Shared.Utils
{
    public class UniqueUserName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (UserDbContext)validationContext.GetService(typeof(UserDbContext))!;
            string userName = value?.ToString()!.ToLower()!;

            var account = context.Accounts.FirstOrDefault(a => a.UserName.Equals(userName));
            if (account != null)
            {
                return new ValidationResult("UserName is used.");
            }

            return ValidationResult.Success;
        }
    }
}
