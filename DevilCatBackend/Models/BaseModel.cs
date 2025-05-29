using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevilCatBackend.Models
{
    public class BaseModel
    {
        [Key]
        [DisplayName("_id")]
        public string ID { get; set; } = default!;

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; }
    }
}
