using System;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Models.Dtos
{
    public class CourseUpdateDto
    {
        [StringLength(100, ErrorMessage = "The {0} length must not exceed {1}.")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int courseId;

        [Required]
        public int PublisherId { get; set; }

        public string Description { get; set; }


    }
}
