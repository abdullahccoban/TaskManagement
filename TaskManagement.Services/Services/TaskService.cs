using AutoMapper;
using TaskManagement.Data;
using TaskManagement.Domain;

namespace TaskManagement.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async System.Threading.Tasks.Task CreateTaskAsync(CreateTaskDto createTask)
    {
        if (createTask == null)
            throw new ArgumentNullException(nameof(createTask), "Task bilgisi boş olamaz.");

        createTask.GroupTaskNumber = await _repo.GetMaxTaskNumber(createTask.GroupId) + 1;
        
        var domain = _mapper.Map<TaskDomain>(createTask);
        var entity = _mapper.Map<Data.Context.Task>(domain);

        await _repo.CreateTask(entity);
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(UpdateTaskDto updateTask)
    {
        if (updateTask == null) 
            throw new ArgumentNullException(nameof(updateTask));

        var existing = await _repo.GetTaskById(updateTask.Id);
        
        if (existing == null)
            throw new KeyNotFoundException("Task bulunamadı.");

        var domain = _mapper.Map<TaskDomain>(existing);
        domain.Update(updateTask.Title, updateTask.Desc, updateTask.UserId, updateTask.StatusId);

        var updatedEntity = _mapper.Map(updateTask, existing);        
        await _repo.UpdateTask(updatedEntity);
    }

    public async System.Threading.Tasks.Task SoftDeleteTaskAsync(int id)
    {
        var task = await _repo.GetTaskById(id);

        if(task == null)
            throw new KeyNotFoundException("Task bulunamadı.");
        
        var domain = _mapper.Map<TaskDomain>(task);
        domain.SoftDelete();

        task.IsDeleted = domain.IsDeleted;
        await _repo.UpdateTask(task);       
    }

    public async System.Threading.Tasks.Task UpdateTaskStatusAsync(int id, int statusId)
    {
        var task = await _repo.GetTaskById(id);

        if(task == null)
            throw new KeyNotFoundException("Task bulunamadı.");

        var domain = _mapper.Map<TaskDomain>(task);
        domain.UpdateStatus(statusId);

        task.StatusId = domain.StatusId;      

        await _repo.UpdateTask(task);
    }

    public async System.Threading.Tasks.Task UpdateTaskUserAsync(int id, int userId)
    {
        var task = await _repo.GetTaskById(id);

        if(task == null)
            throw new KeyNotFoundException("Task bulunamadı.");

        var domain = _mapper.Map<TaskDomain>(task);
        domain.UpdateUser(userId);

        task.UserId = domain.UserId;      

        await _repo.UpdateTask(task);
    }

    public async System.Threading.Tasks.Task<TaskDto> GetTaskByIdAsync(int id)
    {
        var task = await _repo.GetTaskById(id);
        return _mapper.Map<TaskDto>(task);
    }

    public async System.Threading.Tasks.Task<List<TaskDto>> GetGroupTasksAsync(int groupId, int skip, int take, bool pagination = true)
    {
        var tasks = await _repo.GetTasks(groupId, skip, take, pagination);
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async System.Threading.Tasks.Task<List<TaskDto>> GetMyTasksAsync(int userId)
    {
        var myTasks = await _repo.GetMyTasks(userId);
        return _mapper.Map<List<TaskDto>>(myTasks);
    }

    public async System.Threading.Tasks.Task<List<TaskDto>> GetAllTasksAsync()
    {
        var allTasks = await _repo.GetAllTasks();
        return _mapper.Map<List<TaskDto>>(allTasks);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
    {
        await _repo.RemoveTask(id);
    }    
}
