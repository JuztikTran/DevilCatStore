using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace StoreService.Models.Product
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public required string Description { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Required]
        [Range(0, int.MaxValue)]    
        public int Quantity { get; set; }

        [DefaultValue(0)]
        [Range(0, 5)]
        public double Rating { get; set; }


        [DefaultValue(0)]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [DefaultValue(0)]
        [Range(0, 1)]
        public double Discount { get; set; }

        [Required]
        public required string CategoryID { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductVariant> Variants { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
