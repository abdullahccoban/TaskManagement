using Task = TaskManagement.Data.Context.Task;

namespace TaskManagement.Services;

public interface ITaskService
{
    Task<List<Task>> GetAllAsync(string? userId = null);
    Task<Task?> GetByIdAsync(int id, string? userId = null);
    Task<Task> CreateAsync(Task task);
    Task<bool> UpdateAsync(Task task, string? userId = null);
    Task<bool> DeleteAsync(int id, string? userId = null);
}
