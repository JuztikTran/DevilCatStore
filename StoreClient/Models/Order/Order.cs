using System.ComponentModel.DataAnnotations;

namespace StoreService.Models.Order
{
    public class Order : BaseModel
    {
        [Required]
        public required string ProductId { get; set; }

        [Required]
        public required string VariantID { get; set; }

        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double ShippingFee { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Discount { get; set; }
    }
}
