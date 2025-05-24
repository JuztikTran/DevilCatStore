using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CategoryItem
    {
        [Key]
        [StringLength(32)]
        [Required]
        public required string CategoryID { get; set; }

        [Required]
        [StringLength(32)]
        [Key]
        public required string ProductID { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public virtual Category Category { get; set; } = default!;

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; } = default!;
    }
}
