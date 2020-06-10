using AutoMapper;
using Demo.Users.Application.Common.Models;
using Demo.Users.Domain.Entities;

namespace Demo.Users.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
