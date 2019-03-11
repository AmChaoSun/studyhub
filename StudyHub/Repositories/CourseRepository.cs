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
                throw new CustomDbException("course name existed");
            }
            return base.Add(record);
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

        public Course RegisterCourse(CourseRegisterDto info)
        {
            var user = context.Users
                .Include(x => x.Role)
                .Where(x => x.Id == info.PublisherId)
                .FirstOrDefault();
            if(user == null)
            {
                throw new CustomDbException("Publisher not found.");
            }
            if(user.Role.Name == "General")
            {
                throw new UnauthorizedAccessException();
            }

            return Add(info);
        }

        public Course UpdateBasicInfo(Course course, CourseUpdateDto info)
        {
            var entry = context.Entry(course);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return course;
        }
    }
}
