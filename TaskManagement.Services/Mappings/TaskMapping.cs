using AutoMapper;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class TaskMapping : Profile
{
    public TaskMapping()
    {
        // DTO -> Domain
        CreateMap<CreateTaskDto, TaskDomain>()
            .ConstructUsing(src => new TaskDomain(src.Title, src.GroupId, src.StatusId, src.UserId, src.GroupTaskNumber, src.Desc, false, 0));
        
        // DTO -> Domain
        CreateMap<UpdateTaskDto, TaskDomain>()
            .ConstructUsing(src => new TaskDomain(src.Title, src.GroupId, src.StatusId, src.UserId, src.GroupTaskNumber, src.Desc, false ,src.Id));

        // Domain -> Entity
        CreateMap<TaskDomain, TaskManagement.Data.Context.Task>()
            .ReverseMap();
        
        // Entity -> Dto
        CreateMap<TaskManagement.Data.Context.Task, TaskDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Status1))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.GroupName))
            .ForMember(dest => dest.GroupTaskNumber, opt => opt.MapFrom(src => src.GroupTaskNumber))
            .ForMember(dest => dest.DisplayTaskCode, opt => opt.MapFrom(src => src.Group.GroupName + "-" + src.GroupTaskNumber));
    }
}
