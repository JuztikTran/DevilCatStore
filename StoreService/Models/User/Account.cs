using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.User
{
    public class Account : BaseModel
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

        public string ProfileID { get; set; } = default!;
        [ForeignKey(nameof(ProfileID))]
        public virtual Profile Profile { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
