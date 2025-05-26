using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Shared.DTOs
{
    public class DTOProductImage
    {
        [Required]
        [StringLength(32)]
        public required string ProductID { get; set; }

        [Required]
        public required string Image { get; set; }

        [Range(0, int.MaxValue)]
        public int Position { get; set; }
    }
}
