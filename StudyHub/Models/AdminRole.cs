using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class AdminRole
    {
        public AdminRole()
        {
            AdminUsers = new HashSet<AdminUser>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
    }
}
