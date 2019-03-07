using System;
using System.Collections.Generic;
using System.Linq;
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

        public CourseDisplayDto CreateCourse(CourseRegisterDto course)
        {
            var newCourse = mapper.Map<CourseRegisterDto, Course>(course);

            newCourse = courseRepository.Add(newCourse);
            if(newCourse == null)
            {
                throw new CustomDbException("Course name existed.");
            }

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
