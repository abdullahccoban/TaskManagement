using TaskManagement.Services;

namespace TaskManagement.Web;

public class GroupViewModel
{

    public List<GroupDto>? MyGroups { get; set; }
    public GroupDto? GroupDetail { get; set; }
    public List<GroupMemberDto>? GroupMembers { get; set; }
    public List<StatusDto>? Statuses { get; set; }

}
