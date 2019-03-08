using System;
namespace StudyHub.Models.Dtos
{
    public class UserDisplayDto
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
