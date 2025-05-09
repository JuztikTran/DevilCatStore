using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DTOs.User
{
    public class DTOAddress
    {
        [Required]
        public required string AccountId { get; set; } = default!;

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

    public class DTOAddressUpdate : DTOAddress
    {
        [Required]
        public required string ID { get; set; }
    }
}
