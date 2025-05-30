using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevilCatBackend.Models
{
    public class Account : BaseModel
    {
        [Required]
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
        [Required]
        public required string Email { get; set; }

        public string? Avatar { get; set; }

        [StringLength(25)]
        [DefaultValue("User")]
        public string? Role { get; set; }

        [StringLength(50)]
        [DefaultValue("Local")]
        public string? AccountType { get; set; }

        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        public string? Reason { get; set; }

        public string? GoogleID { get; set; }
        public string? FacebookeID { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = default!;
    }
}
