using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DTOs.Product
{
    public class DTOProductVariant
    {
        [Required]
        public required string ProductID { get; set; }

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

    public class DTOProductVariantUpdate : DTOProductVariant
    {
        [Required]
        public required string ID { get; set; }
    }
}
