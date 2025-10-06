using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public class UserRequestRepository : IUserRequestRepository
{
    private readonly TaskManagementDbContext _context;

    public UserRequestRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserRequest>> GetUserRequestsByGroupId(int groupId)
        => await _context.UserRequests.Include(ur => ur.User).Where(i => i.GroupId == groupId).ToListAsync();

    public async Task CreateUserRequest(UserRequest userRequest)
    {
        _context.UserRequests.Add(userRequest);
        await _context.SaveChangesAsync();
    }    

    public async Task RemoveUserRequest(int id)
    {
        var userRequest = await _context.UserRequests.FirstOrDefaultAsync(i => i.Id == id);

        if (userRequest != null) 
        {
            _context.UserRequests.Remove(userRequest);
            await _context.SaveChangesAsync();
        }
    }
}
