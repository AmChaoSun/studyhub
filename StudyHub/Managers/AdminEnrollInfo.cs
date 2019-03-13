using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Managers
{
    public class AdminEnrollInfo
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
