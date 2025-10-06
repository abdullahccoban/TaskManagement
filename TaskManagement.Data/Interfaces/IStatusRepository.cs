using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public interface IStatusRepository
{
    Task<List<Status>?> GetStatuses(int groupId);
    Task<Status?> GetStatusById(int id);
    Task CreateStatus(Status status);
    Task UpdateStatus(Status status);
    Task RemoveStatus(int id);
}
