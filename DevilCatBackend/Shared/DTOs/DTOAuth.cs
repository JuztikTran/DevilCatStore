using DevilCatBackend.Shared.Validator;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevilCatBackend.Shared.DTOs
{
    public class DTOSignIn
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }

    public class DTOSignUp
    {
        [Required]
        [UniqueUserName]
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string Password { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [EmailAddress]
        [UniqueEmail]
        [Required]
        public required string Email { get; set; }

        public string? GoogleID { get; set; }
        public string? FacebookeID { get; set; }
    }

    public class DTOForgotPass
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        [Length(8, 32)]
        public string? NewPassword { get; set; }
    }
}
