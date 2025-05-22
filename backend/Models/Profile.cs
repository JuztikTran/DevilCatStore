using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Profile
    {
        [Key]
        public required string ID { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(25)]
        [DefaultValue("Other")]
        public required string Gender { get; set; }

        [Required]
        public required string Avatar { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Account Account { get; set; } = default!;
    }
}
