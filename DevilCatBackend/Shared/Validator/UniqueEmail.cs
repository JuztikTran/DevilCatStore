using DevilCatBackend.Data;
using System.ComponentModel.DataAnnotations;

namespace DevilCatBackend.Shared.Validator
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (UserDbContext)validationContext.GetService(typeof(UserDbContext))!;
            string email = value?.ToString()!;

            bool exist = context.Accounts.Where(x => x.Email == email).Any();
            if (exist)
            {
                return new ValidationResult("Email has been existed.");
            }

            return ValidationResult.Success;
        }
    }
}
