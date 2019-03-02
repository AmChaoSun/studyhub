using System;
using System.Collections.Generic;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface ICourseManager
    {
        CourseDisplayDto GetCourseById(int id);
        CourseDisplayDto CreateCourse(CourseRegisterDto course);
        CourseDisplayDto UpdateCourse(int id, CourseUpdateDto info);
        void DeleteCourse(int id, int userId);
        IEnumerable<UserDisplayDto> GetStudentsByCourse(int id, int userId);
    }
}
