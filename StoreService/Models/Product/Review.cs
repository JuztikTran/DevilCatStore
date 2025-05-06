using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Product
{
    public class Review : BaseModel
    {
        [Required]
        public required string AccountID { get; set; }

        [Required]
        public required string Content { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        public required string VariantID { get; set; }

        [ForeignKey(nameof(VariantID))]
        public virtual ProductVariant ProductVariant { get; set; }

        [Required]
        public required string ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; }
    }
}
