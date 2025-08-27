using AutoMapper;
using TaskManagement.Data.Context;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<UserDomain, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
    }

}
