using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class UserEditDto
    {
        [Required]
        public int Id;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string NickName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string Role { get; set; }

        [Required]
        [Range(typeof(bool),"true", "true", ErrorMessage ="The field Is Active must be checked.")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string Mobile { get; set; }
    }
}
