using System;
using System.Collections.Generic;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface IUserManager
    {
        UserDisplayDto GetUserById(int id);
        UserDisplayDto UpdateUser(UserUpdateDto info);
        UserSearchResultDto GetUsers(UserSearchAttribute info);
        void DeleteUser(int id);
        IEnumerable<CourseDisplayDto> GetEnrolledCourses(int studentId);
        void StudentEnrollCourse(int studentId, int courseId);
        void StudentUnenrollCourse(int studentId, int courseId);
    }
}
