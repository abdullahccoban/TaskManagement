namespace TaskManagement.Data;

public interface ITaskRepository
{
    System.Threading.Tasks.Task CreateTask(Context.Task groupTask);
    System.Threading.Tasks.Task UpdateTask(Context.Task groupTask);
    System.Threading.Tasks.Task<List<Context.Task>?> GetTasks(int groupId);
    System.Threading.Tasks.Task<Context.Task?> GetTaskById(int id);    
    System.Threading.Tasks.Task<List<Context.Task>?> GetAllTasks();
    System.Threading.Tasks.Task RemoveTask(int id);
    System.Threading.Tasks.Task<List<Context.Task>?> GetMyTasks(int userId);
    System.Threading.Tasks.Task<int> GetMaxTaskNumber(int groupId);    
}
