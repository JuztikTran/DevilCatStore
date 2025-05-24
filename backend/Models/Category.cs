using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Category
    {
        [Key]
        [StringLength(32)]
        public required string ID { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<CategoryItem> CategoryItems { get; set; } = default!;
    }
}
