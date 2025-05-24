using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ProductVarriant
    {
        [Key]
        [StringLength(32)]
        public required string ID { get; set; }

        [Required]
        [StringLength(32)]
        public required string ProductID { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Required]
        [StringLength(15)]
        public required string Size { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "numeric(3)")]
        public double Price { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; } = default!;
    }
}
