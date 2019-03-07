using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class Enroll
    {
        public int EnrollId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
