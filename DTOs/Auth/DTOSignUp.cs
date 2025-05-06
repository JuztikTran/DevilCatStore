using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.Auth
{
    public enum Role
    {
        ADMIN = 0,
        USER = 1,
        STAFF = 2
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2,
        Unknow = 3
    }

    public class DTOSignUp
    {
        [Required]
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string Password { get; set; }

        [DefaultValue(false)]
        public bool IsBan { get; set; }

        public string RefreshToken { get; set; } = default!;

        [DefaultValue("LOCAL")]
        public string AccountType { get; set; } = default!;

        [DefaultValue(Role.USER)]
        public required Role Role { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        public string? AvatarURI { get; set; }

        [DefaultValue(Gender.Unknow)]
        public Gender Gender { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateOnly DateOfBirth { get; set; }
    }
}
