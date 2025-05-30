using DevilCatBackend.Data;
using System.ComponentModel.DataAnnotations;

namespace DevilCatBackend.Shared.Validator
{
    public class UniqueUserName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (UserDbContext)validationContext.GetService(typeof(UserDbContext))!;
            string username = value?.ToString()!;

            bool exist = context.Accounts.Where(a => a.UserName.ToLower() == username.ToLower()).Any();
            if (exist)
            {
                return new ValidationResult("UserName has been exist.");
            }
            return ValidationResult.Success;
        }
    }
}
