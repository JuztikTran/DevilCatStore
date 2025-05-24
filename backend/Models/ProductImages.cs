using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ProductImages
    {
        [Key]
        [StringLength(32)]
        public required string ID { get; set; }

        [Required]
        [StringLength(32)]
        public required string ProductID { get; set; }

        [Required]
        public required string Image { get; set; }

        public int Position { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; } = default!;
    }
}
