using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class UserAuth
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IdentityType { get; set; }
        public string Identifier { get; set; }
        public string Credential { get; set; }
        public bool IsVerified { get; set; }
        public bool InSite { get; set; }

        public virtual User User { get; set; }
    }
}
