using System;
namespace StudyHub.Models.Dtos
{
    public class CourseDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
