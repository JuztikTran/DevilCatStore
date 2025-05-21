using backend.Shared.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Account
    {
        [Key]
        [StringLength(24)]
        [Length(6, 24)]
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

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }
    }
}
