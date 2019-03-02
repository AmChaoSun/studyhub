using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string IdentityType { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public string Credential { get; set; }
    }
}
