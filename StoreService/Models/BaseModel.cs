using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreService.Models
{

    public class BaseModel
    {
        [Key]
        public string ID { get; set; } = new Guid().ToString("N");
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
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
