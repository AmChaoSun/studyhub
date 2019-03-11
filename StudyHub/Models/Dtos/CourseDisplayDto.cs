using System;
namespace StudyHub.Models.Dtos
{
    public class CourseDisplayDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LecturerDisplayDto Lecturer { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
