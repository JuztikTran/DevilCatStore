using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.User
{
    public class Profile : BaseModel
    {
        public required string AccountID { get; set; }
        [ForeignKey(nameof(AccountID))]
        public virtual Account Account { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        public string? AvatarURI { get; set; }

        [DefaultValue(Gender.Unknow)]
        public Gender Gender { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateOnly DateOfBirth { get; set; }
    }
}
