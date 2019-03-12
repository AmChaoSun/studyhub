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
        void DeleteCourse(int coureId, int userId);
        IEnumerable<UserDisplayDto> GetEnrolledStudents(int courseId, int userId);
        IEnumerable<CourseDisplayDto> AdminGetAllCourses();
        CourseDisplayDto AdminGetCourseById(int courseId);
        CourseDisplayDto AdminRegisterCourse(CourseRegisterDto courseInfo);
        CourseDisplayDto UpdateCourse(CourseUpdateDto info);
        void AdminDeleteCourse(int courseId);
    }
}
