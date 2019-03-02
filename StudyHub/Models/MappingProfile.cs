using System;
using AutoMapper;
using StudyHub.Models.Dtos;

namespace StudyHub.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user
            CreateMap<User, UserDisplayDto>();

            //course
            CreateMap<CourseRegisterDto, Course>()
                .ForMember(d => d.CreatedOn,
                    o => o.MapFrom(src => DateTime.Now));
            CreateMap<Course, CourseDisplayDto>();
        }
    }
}
