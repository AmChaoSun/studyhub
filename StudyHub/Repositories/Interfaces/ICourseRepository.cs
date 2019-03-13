using System;
using System.Collections.Generic;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IEnumerable<User> GetEnrolledStudents(int courseId);
        Course AdminGetCourseById(int courseId);
        Course RegisterCourse(Course course);
        Course UpdateCourse(CourseUpdateDto info);
        void AdminDeleteCourse(int courseId);
    }
}
