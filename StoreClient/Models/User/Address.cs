using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.User
{
    public class Address : BaseModel
    {
        public string AccountId { get; set; } = default!;

        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }

        [Required]
        [StringLength(50)]
        public required string ReceiverName { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public required string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsDefault { get; set; }

        [Required]
        public required string Location { get; set; }

        public string Note { get; set; } = default!;
    }
}
