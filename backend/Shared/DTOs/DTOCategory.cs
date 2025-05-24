using System.ComponentModel.DataAnnotations;

namespace backend.Shared.DTOs
{
    public class DTOCategory
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
    }

    public class DTODeleteCategory
    {
        public string[]? ID { get; set; }
    }
}
