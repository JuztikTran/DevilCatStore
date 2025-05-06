using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Product
{
    public class ProductVariant : BaseModel
    {
        [Required]
        public required string ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; }

        [Required]
        public required string Color { get; set; }

        [Required]
        public required string Size { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Range(0, 1)]
        [DefaultValue(0)]
        public double Discount { get; set; }
    }
}
