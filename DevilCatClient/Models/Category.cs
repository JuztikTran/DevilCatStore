using System.ComponentModel.DataAnnotations;

namespace DevilCatClient.Models
{
    public class Category : BaseModel
    {
        [StringLength(50)]
        public string? Name { get; set; }

        public string? ParentID { get; set; }

        public virtual Category? Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; } = default!;

    }
}
