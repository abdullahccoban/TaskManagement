namespace TaskManagement.Services;

public interface IGroupService
{
    Task CreateGroupAsync(string groupName, int userId);
    Task<List<GroupDto>> GetMyGroups(int userId);
    Task<GroupDto> GetGroupDetail(int id);
    Task<List<GroupMemberDto>> GetGroupMembers(int groupId);
    Task UpdateGroupAsync(int id, string groupName, int userId);
    Task RemoveGroupAsync(int id);
    Task<List<GroupDto>> GetAllGroups();
    Task<List<UserRequestDto>> GetAllUserRequests(int groupId);
    Task AddGroupMember(int groupId, int userId);
    Task RemoveGroupMember(int id);
    Task RemoveRequest(int groupId, int userId);
}
