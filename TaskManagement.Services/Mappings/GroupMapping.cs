using AutoMapper;
using TaskManagement.Data.Context;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class GroupMapping : Profile
{
    public GroupMapping()
    {
        CreateMap<Group, GroupDto>();
        CreateMap<GroupDto, Group>();

        CreateMap<GroupDomain, Group>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName));
        
        CreateMap<GroupMemberDomain, GroupMember>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UserRequestDto, UserRequest>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { Username = src.Username }))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UserRequest, UserRequestDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UserRequestDomain, UserRequest>();
    }

}
