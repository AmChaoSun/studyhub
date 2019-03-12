using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        public string NickName { get; set; }
    }
}
