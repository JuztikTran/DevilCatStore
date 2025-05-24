using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Product
    {
        [Key]
        [StringLength(32)]
        public required string ID { get; set; }

        [Required]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Range(0, 1)]
        [Column(TypeName = "numeric(2)")]
        public double Discount { get; set; }

        [Range(0, 5)]
        [Column(TypeName = "numeric(1)")]
        public double Rating { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<CategoryItem> CategoryItems { get; set; } = default!;
        public virtual ICollection<ProductImages> Images { get; set; } = default!;
        public virtual ICollection<ProductVarriant> Varriants { get; set; } = default!;
    }
}
