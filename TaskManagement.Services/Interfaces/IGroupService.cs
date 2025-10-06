namespace TaskManagement.Services;

public interface IGroupService
{
    Task CreateGroupAsync(string groupName, int userId);
    Task<List<GroupDto>> GetMyGroups(int userId);
    Task<GroupDto> GetGroupDetail(int id);
    Task<List<GroupMemberDto>> GetGroupMembers(int groupId);
}
