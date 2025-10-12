namespace TaskManagement.Services;

public interface ITaskService
{
    Task CreateTaskAsync(CreateTaskDto createTask); //groupAdmin
    Task UpdateTaskAsync(UpdateTaskDto updateTask); //groupAdmin
    Task SoftDeleteTaskAsync(int id); //groupAdmin
    Task UpdateTaskStatusAsync(int id, int statusId); //groupAdmin, groupUser
    Task UpdateTaskUserAsync(int id, int userId); //groupAdmin
    Task<TaskDto> GetTaskByIdAsync(int id); //groupAdmin, groupUser
    Task<List<TaskDto>> GetGroupTasksAsync(int groupId, int skip, int take, bool pagination = true); //groupAdmin, groupUser
    Task<List<TaskDto>> GetMyTasksAsync(int userId); //groupAdmin, groupUser
    Task<List<TaskDto>> GetAllTasksAsync(); //admin
    Task DeleteTaskAsync(int id); //admin
}
