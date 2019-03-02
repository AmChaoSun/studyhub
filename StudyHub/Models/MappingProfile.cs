using System;
using AutoMapper;
using StudyHub.Models.Dtos;

namespace StudyHub.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDisplayDto>();
        }
    }
}
