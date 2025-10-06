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
    }

}
