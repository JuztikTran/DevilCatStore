using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Product
{
    public class ProductImage : BaseModel
    {
        [Required]
        public required string ImageURI { get; set; }

        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        public required string ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
