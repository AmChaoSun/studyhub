﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Managers;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StudyHubContext context) : base(context)
        {

        }

        public override User GetById(int id)
        {
            return Records
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == id);
        }
        public User EditUser(UserEditDto info)
        {
            var role = context.UserRoles
                .Where(x => x.Name == info.Role)
                .FirstOrDefault();

            if(role == null)
            {
                throw new CustomDbException("Invalid role value");
            }

            var user = Records
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == info.Id);

            if (user == null)
            {
                throw new CustomDbException("User not Found");
            }

            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(new { 
                info.NickName,
                info.Mobile,
                info.IsActive,
                info.Email,
                role.RoleId,
                Role = role
            });
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        public IEnumerable<Course> GetEnrolledCourses(int studentId)
        {
            if (Records.Find(studentId) == null)
            {
                throw new CustomDbException("Student not found.");
            }
            var courses = context.Enrolls
                .Where(x => x.UserId == studentId)
                .Include(x => x.Course)
                    .ThenInclude(x => x.Publisher)
                .Select(x => x.Course);
            return courses;
        }

        public IEnumerable<string> GetRoles()
        {
            return context.UserRoles
                .Select(x => x.Name);
        }

        public IEnumerable<User> GetUsers(UserSearchAttribute info)
        {
            var users = Records.Include(x => x.Role)
                .Search(info.SearchValue)
                .ApplySort(info.SortString, info.SortOrder);
            if(info.Role != null)
            {
                users = users.Where(x => x.Role.Name == info.Role);
            }
            return users;
        }

        public void StudentEnrollCourse(int studentId, int courseId)
        {
            if(context.Enrolls
                .Any(x => x.CourseId == courseId && x.UserId == studentId))
            {
                throw new CustomDbException("Already enrolled.");
            }
            if(!context.Courses.Any(x => x.CourseId == courseId))
            {
                throw new CustomDbException("Course not found.");
            }
            if(!Records.Any(x => x.Id == studentId))
            {
                throw new CustomDbException("Student not found.");
            }

            context.Enrolls.Add(new Enroll
            {
                UserId = studentId,
                CourseId = courseId
            });
            context.SaveChanges();
        }

        public void StudentUnenrollCourse(int studentId, int courseId)
        {
            var enroll = context.Enrolls
                .Where(x => x.CourseId == courseId && x.UserId == studentId)
                .FirstOrDefault();
            if (enroll == null)
            {
                throw new CustomDbException("Enroll record not found.");
            }
            context.Remove(enroll);
            context.SaveChanges();
        }

        public User UpdateBasicInfo(User user, UserUpdateDto info)
        {
            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        public User UpdateUser(UserUpdateDto info)
        {
            var user = Records
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == info.Id);

            if(user == null)
            {
                throw new CustomDbException("User not Found");
            }

            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }



    }
}
