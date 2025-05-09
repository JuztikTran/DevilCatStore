using System.ComponentModel.DataAnnotations;

namespace DTOs.User
{
    public class DTOChangePassword
    {
        [Required]
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string OldPassword { get; set; }

        [Required]
        [Length(8, 32)]
        public required string NewPassword { get; set; }
    }
}
