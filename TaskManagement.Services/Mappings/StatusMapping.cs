using AutoMapper;
using TaskManagement.Data.Context;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class StatusMapping : Profile
{
    public StatusMapping()
    {
        CreateMap<Status, StatusDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status1));
        CreateMap<StatusDto, Status>()
            .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status));;

        CreateMap<StatusDomain, Status>()
                .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId));
    }
}
