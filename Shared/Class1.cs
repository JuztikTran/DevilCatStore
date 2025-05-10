using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class DataValidation : ValidationAttribute
    {
        public override bool IsValid(object? value, ValidationContext context)
        {
            return base.IsValid(value);
        }
    }
}
