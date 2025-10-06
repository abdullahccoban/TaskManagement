using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public interface IUserRequestRepository
{
    Task<List<UserRequest>> GetUserRequestsByGroupId(int groupId);
    Task CreateUserRequest(UserRequest userRequest);
    Task RemoveUserRequest(int id);
}
