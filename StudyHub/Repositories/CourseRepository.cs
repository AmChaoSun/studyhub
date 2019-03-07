using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;

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
                return null;
            }
            return base.Add(record);
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
