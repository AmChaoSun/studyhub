using System;
using System.Collections.Generic;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface ICourseManager
    {
        CourseDisplayDto GetCourseById(int courseId);
        CourseDisplayDto CreateCourse(CourseRegisterDto course);
        CourseDisplayDto UpdateCourse(int courseId, CourseUpdateDto info);
        void DeleteCourse(int coureId, int userId);
        IEnumerable<UserDisplayDto> GetEnrolledStudents(int courseId, int userId);
        IEnumerable<CourseDisplayDto> AdminGetAllCourses();
        CourseDisplayDto AdminGetCourseById(int courseId);
        CourseDisplayDto AdminRegisterCourse(CourseRegisterDto courseInfo);

    }
}
