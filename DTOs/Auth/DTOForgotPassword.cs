using System.ComponentModel.DataAnnotations;

namespace DTOs.Auth
{
    public class DTOForgotPassword
    {
        [Required]
        [Length(6, 24)]
        public required string UserName { get; set; }

        [Required]
        [Length(8, 32)]
        public required string Password { get; set; }
    }
}
