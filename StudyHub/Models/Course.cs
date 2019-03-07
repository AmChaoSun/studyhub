using System;
using System.Collections.Generic;

namespace StudyHub.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrolls = new HashSet<Enroll>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User Publisher { get; set; }
        public virtual ICollection<Enroll> Enrolls { get; set; }
    }
}
