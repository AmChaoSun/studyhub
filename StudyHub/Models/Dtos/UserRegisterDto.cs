using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string NickName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string IdentityType { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string Identifier { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} length must between {1} and {2}.", MinimumLength = 8)]
        public string Credential { get; set; }

        [Required]
        [Compare("Credential", ErrorMessage = "Password and ConfirmPassword must be the same.")]
        public string ConfirmedCredential { get; set; }
    }
}
