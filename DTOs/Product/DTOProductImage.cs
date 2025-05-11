using System.ComponentModel.DataAnnotations;

namespace DTOs.Product
{
    public class DTOProductImage
    {
        [Required]
        public required string ImageURI { get; set; }

        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        [Required]
        public required string ProductId { get; set; }
    }

    public class DTOProductImageUpdate : DTOProductImage
    {
        [Required]
        public required string ID { get; set; }
    }
}
