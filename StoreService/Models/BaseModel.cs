using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models
{

    public class BaseModel
    {
        [Key]
        public string ID { get; set; } = new Guid().ToString("N");

        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Column(TypeName = "TIMESTAMP")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }

    public enum Role
    {
        ADMIN = 0,
        USER = 1,
        STAFF = 2
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2,
        Unknow = 3
    }

}
