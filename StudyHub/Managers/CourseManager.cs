using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Managers
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        public CourseManager(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CourseDisplayDto> AdminGetAllCourses()
        {
            var courses = courseRepository.Records
                .Include(x => x.Publisher)
                .Select(x => new CourseDisplayDto 
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Lecturer = new LecturerDisplayDto
                    {
                        Name = x.Publisher.NickName
                    }
                });
            return courses;
        }

        public CourseDisplayDto AdminGetCourseById(int courseId)
        {
            var course = courseRepository.AdminGetCourseById(courseId);
            if(course == null)
            {
                return null;
            }

            var displayCourse = new CourseDisplayDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description,
                CreatedOn = course.CreatedOn,
                Lecturer = new LecturerDisplayDto
                {
                    Name = course.Publisher.NickName
                }
            };
            return displayCourse;

        }

        public CourseDisplayDto AdminRegisterCourse(CourseRegisterDto courseInfo)
        {
            var course = mapper.Map<CourseRegisterDto, Course>(courseInfo);
            course  = courseRepository.Add(course);
            var displayCourse = mapper.Map<Course, CourseDisplayDto>(course);
            return displayCourse;
        }

        public CourseDisplayDto CreateCourse(CourseRegisterDto course)
        {
            var newCourse = mapper.Map<CourseRegisterDto, Course>(course);

            newCourse = courseRepository.Add(newCourse);

            var displayCourse = mapper.Map<Course, CourseDisplayDto>(newCourse);
            return displayCourse;
        }

        public void DeleteCourse(int courseId, int userId)
        {
            var course = courseRepository.Records
                .Where(x => x.CourseId == courseId && x.PublisherId == userId)
                .FirstOrDefault();
            if(course == null)
            {
                throw new CustomDbException("Invalid request");
            }
            courseRepository.Delete(course);
        }

        public CourseDisplayDto GetCourseById(int courseId)
        {
            var course = courseRepository.GetById(courseId);
            if(course == null)
            {
                throw new CustomDbException("Course not Found");
            }

            var displayCourse = mapper.Map<Course, CourseDisplayDto>(course);
            return displayCourse;
        }

        public IEnumerable<UserDisplayDto> GetEnrolledStudents(int courseId, int userId)
        {
            var course = courseRepository.Records
                .Where(x => x.CourseId == courseId && x.PublisherId == userId)
                .FirstOrDefault();
            if (course == null)
            {
                throw new CustomDbException("Invalid request");
            }
            var students = courseRepository.GetEnrolledStudents(course);
            var displayStudents = mapper.Map<IEnumerable<User>,
                                    IEnumerable<UserDisplayDto>>(students);
            return displayStudents;

        }

        public CourseDisplayDto UpdateCourse(int courseId, CourseUpdateDto info)
        {
            var course = courseRepository.Records
                .Where(x => x.CourseId == courseId && x.PublisherId == info.PublisherId)
                .FirstOrDefault();
            if (course == null)
            {
                throw new CustomDbException("Invalid request");
            }

            course = courseRepository.UpdateBasicInfo(course, info);
            var displayCourse = mapper.Map<Course, CourseDisplayDto>(course);
            return displayCourse;
        }
    }
}
