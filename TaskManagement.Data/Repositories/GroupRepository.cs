using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public class GroupRepository : IGroupRepository
{
    private readonly TaskManagementDbContext _context;

    public GroupRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Group> AddAsync(Group group)
    {
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
        return group;
    }

    public async Task AddGroupMember(GroupMember groupMember)
    {
        _context.GroupMembers.Add(groupMember);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Group>> GetAllAsync() 
        => await _context.Groups.ToListAsync();

    public async Task<Group?> GetByIdAsync(int id) 
        => await _context.Groups.FindAsync(id);

    public async Task<List<GroupMember>> GetGroupMembers(int groupId) 
        => await _context.GroupMembers.Where(i => i.GroupId == groupId).Include(gm => gm.User).ToListAsync();

    public async Task<List<Group>> GetMyGroupsAsync(int id) 
        => await _context.Groups.Where(i => i.CreatedUserId == id).ToListAsync();

    public async Task<Group> UpdateAsync(Group group)
    {
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
        return group;
    }
}
