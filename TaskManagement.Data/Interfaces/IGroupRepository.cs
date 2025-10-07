using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public interface IGroupRepository
{
    Task<List<Group>> GetAllAsync();
    Task<List<Group>> GetMyGroupsAsync(int id);
    Task<Group?> GetByIdAsync(int id);
    Task<Group> AddAsync(Group group);
    Task<Group> UpdateAsync(Group group);
    Task RemoveAsync(int id);
    Task AddGroupMember(GroupMember groupMember);
    Task<List<GroupMember>> GetGroupMembers(int groupId);
    Task RemoveGroupMembers(int id);
    Task<List<Group>> GetGroupsNotJoined(int id);
    Task<List<Group>> GetGroupsJoined(int id);
}
