using System;
namespace StudyHub.Models.Dtos
{
    public class UserLoginDto
    {
        public string IdentityType { get; set; }
        public string Identifier { get; set; }
        public string Credential { get; set; }
    }
}
