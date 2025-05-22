using System.ComponentModel.DataAnnotations;

namespace backend.Shared.DTOs
{
    public class DTOBanAccount
    {
        [Required]
        public required string ID { get; set; }

        [Required]
        public required string Reason { get; set; }
    }
}
