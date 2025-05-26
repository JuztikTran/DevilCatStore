using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace backend.Shared.DTOs
{
    public class DTOProduct
    {
        [Required]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required]
        public required string Thumbnail { get; set; }

        [Range(0, 1)]
        [DefaultValue(0)]
        public double Discount { get; set; }

        [Range(0, 5)]
        [DefaultValue(0)]
        public double Rating { get; set; }
    }

}
