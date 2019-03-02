using System;
using System.Collections.Generic;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Course UpdateBasicInfo(Course course, CourseUpdateDto info);
        IEnumerable<User> GetStudentsByCourse(Course course);
    }
}
