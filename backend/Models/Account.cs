using backend.Shared.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Account
    {
        [Key]
        public required string ID { get; set; }

        [StringLength(24)]
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string Password { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public required string Email { get; set; }

        [DefaultValue("LOCAL")]
        [StringLength(50)]
        public required string AccountType { get; set; }

        [DefaultValue("User")]
        [StringLength(50)]
        public required string Role { get; set; }

        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        public string? Reason { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public string? GoogleId { get; set; }

        public string? FacebookId { get; set; }

        [Column(TypeName = "tim1estamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }
    }
}
