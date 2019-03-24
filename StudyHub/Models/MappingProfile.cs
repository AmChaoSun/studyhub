using System;
using AutoMapper;
using StudyHub.Models.Dtos;

namespace StudyHub.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //admin
            CreateMap<AdminUser, AdminDisplayDto>()
                .ForMember(d => d.Role,
                    o => o.MapFrom(src => src.Role.Name));

            //user
            CreateMap<User, UserDisplayDto>()
                .ForMember(d => d.Role,
                    o => o.MapFrom(src => src.Role.Name));

            //course
            CreateMap<CourseRegisterDto, Course>()
                .ForMember(d => d.CreatedOn,
                    o => o.MapFrom(src => DateTime.Now));

            CreateMap<Course, CourseDisplayDto>()
                .ForMember(d => d.Lecturer,
                    o => o.MapFrom(src => new LecturerDisplayDto {
                        Name = src.Publisher.NickName 
                    }));
        }
    }
}
