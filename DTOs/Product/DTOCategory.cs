using System.ComponentModel.DataAnnotations;

namespace DTOs.Product
{
    public class DTOCategory
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public string? ParentID { get; set; }
    }

    public class DTOCategoryUpdate
    {
        [Required]
        public required string ID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public string? ParentID { get; set; }
    }
}
