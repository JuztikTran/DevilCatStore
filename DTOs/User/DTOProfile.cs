using DTOs.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DTOs.User
{
    public class DTOProfile
    {
        [Required]
        public required string AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        public string? AvatarURI { get; set; }

        [DefaultValue(Gender.Unknow)]
        public Gender Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }

    public class DTOProfileUpdate : DTOProfile
    {
        [Required]
        public required string ID { get; set; }
    }
}
