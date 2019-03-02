using System;
using System.Collections.Generic;
using AutoMapper;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;

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

        public CourseDisplayDto CreateCourse(int userId, CourseRegisterDto course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public CourseDisplayDto GetCourseById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDisplayDto> GetStudentsByCourse(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public CourseDisplayDto UpdateCourse(int id, CourseUpdateDto info)
        {
            throw new NotImplementedException();
        }
    }
}
