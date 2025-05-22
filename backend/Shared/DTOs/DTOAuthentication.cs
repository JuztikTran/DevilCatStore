using backend.Shared.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Shared.DTOs
{
    public class DTOSignIn
    {
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Length(8, 32)]
        public required string Password { get; set; }
    }

    public class DTOSignUp
    {
        [Length(6, 24)]
        [UniqueUserName]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string Password { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        [UniqueEmail]
        public required string Email { get; set; }

        [DefaultValue("LOCAL")]
        [StringLength(50)]
        public required string AccountType { get; set; }

        [DefaultValue("User")]
        [StringLength(50)]
        public required string Role { get; set; }

        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        public string? GoogleId { get; set; }

        public string? FacebookId { get; set; }
    }

    public class DTOForgotPassword
    {
        [Required]
        public required string UserName{ get; set; }

        [Required]
        public required string NewPassword { get; set; }
    }
}
