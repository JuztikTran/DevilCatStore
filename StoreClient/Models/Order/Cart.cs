using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Order
{
    public class Cart : BaseModel
    {
        [Required]
        public required string ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }

        [Required]
        public required string VariantId { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
