using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class AdminLoginDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
