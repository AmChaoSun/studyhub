using System;
using System.Collections.Generic;
using StudyHub.Managers;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User UpdateBasicInfo(User user, UserUpdateDto info);
        IEnumerable<User> GetUsers(UserSearchAttribute info);
        User UpdateUser(UserUpdateDto info);
        IEnumerable<Course> GetEnrolledCourses(int studentId);
        void StudentEnrollCourse(int studentId, int courseId);
        void StudentUnenrollCourse(int studentId, int courseId);
    }
}
