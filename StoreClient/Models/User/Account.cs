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
        [Column(TypeName = "BOOLEAN")]
        public bool IsBan { get; set; }

        public string? RefreshToken { get; set; }

        [DefaultValue("LOCAL")]
        public string? AccountType { get; set; }

        [DefaultValue(Role.USER)]
        [Column(TypeName = "INTEGER")]
        public required Role Role { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = default!;
    }
}
