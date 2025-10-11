using TaskManagement.Services;

namespace TaskManagement.Web;

public class TaskViewModel
{
    public List<TaskDto>? GroupTasks { get; set; } 
    public List<StatusDto>? Statuses { get; set; }
    public List<GroupMemberDto>? GroupMembers { get; set; }
    public GroupDto? GroupDetails { get; set; }   
}
