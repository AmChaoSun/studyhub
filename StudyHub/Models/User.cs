using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class User
    {
        public User()
        {
            Courses = new HashSet<Course>();
            Enrolls = new HashSet<Enroll>();
            UserLoginAuths = new HashSet<UserLoginAuth>();
        }

        public int Id { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Enroll> Enrolls { get; set; }
        public virtual ICollection<UserLoginAuth> UserLoginAuths { get; set; }
    }
}
