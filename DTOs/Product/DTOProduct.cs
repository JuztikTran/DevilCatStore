using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DTOs.Product
{
    public class DTOProduct
    {
        [Required]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required]
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
    }

    public class DTOProductUpdate : DTOProduct
    {
        [Required]
        public required string ID { get; set; }
    }
}
