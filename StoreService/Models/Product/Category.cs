using System.ComponentModel.DataAnnotations;

namespace StoreService.Models.Product
{
    public class Category : BaseModel
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public string? ParentID { get; set; }
        public virtual Category Parent { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
