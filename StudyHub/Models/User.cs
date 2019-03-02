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
            UserAuths = new HashSet<UserAuth>();
        }

        public int Id { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
        public short Role { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Enroll> Enrolls { get; set; }
        public virtual ICollection<UserAuth> UserAuths { get; set; }
    }
}
