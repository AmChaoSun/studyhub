using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class AdminUser
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual AdminRole Role { get; set; }
    }
}
