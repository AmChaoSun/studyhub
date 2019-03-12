using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudyHubContext context): base(context)
        {
        }

        public override Course Add(Course record)
        {
            if(Records.Any(x => x.Name == record.Name))
            {
                throw new CustomDbException("Course name existed.");
            }
            return base.Add(record);
        }

        public override void Delete(Course record)
        {
            base.Delete(record);
        }


        public Course AdminGetCourseById(int courseId)
        {
            var course = Records
                .Include(x => x.Publisher)
                .FirstOrDefault(x => x.CourseId == courseId);
            return course;
        }

        public IEnumerable<User> GetEnrolledStudents(Course course)
        {
            var students = course.Enrolls
                .Join(context.Users,
                            e => e.UserId,
                            u => u.Id,
                            (e, u) => u);
                //.ToList();
            return students;
        }

        public Course RegisterCourse(Course course)
        {
            var user = context.Users
                .Include(x => x.Role)
                .Where(x => x.Id == course.PublisherId)
                .FirstOrDefault();
            if (!CanPublish(user))
            {
                throw new UnauthorizedAccessException();
            }

            if (Records.Any(x => x.Name == course.Name))
            {
                throw new CustomDbException("Course name existed.");
            }

            course.Publisher = user;
            context.Add(course);
            context.SaveChanges();
            return course;
        }

        public Course UpdateCourse(CourseUpdateDto info)
        {
            //check publisher has auth to publish
            var user = context.Users
                .Include(x => x.Role)
                .Where(x => x.Id == info.PublisherId)
                .FirstOrDefault();
            if (!CanPublish(user))
            {
                throw new UnauthorizedAccessException();
            }

            //check course to be updated exists
            var course = Records
                .Where(x => x.CourseId == info.CourseId && x.PublisherId == info.PublisherId)
                .FirstOrDefault();
            if (course == null)
            {
                throw new UnauthorizedAccessException();
            }

            //check values
            if (Records.Any(x => x.Name == info.Name))
            {
                throw new CustomDbException("course name existed");
            }

            //update
            var entry = context.Entry(course);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return course;
        }

        private bool CanPublish(User user)
        {
            if (user == null)
            {
                throw new CustomDbException("Publisher not found.");
            }
            if (user.Role.Name == "General")
            {
                throw new UnauthorizedAccessException();
            }
            return true;
        }

        public void AdminDeleteCourse(int courseId)
        {
            var course = Records.Find(courseId);
            if(course == null)
            {
                throw new CustomDbException("Course not found.");
            }
            Delete(course);
        }
    }
}
