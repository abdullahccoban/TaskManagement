using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;

namespace TaskManagement.Data;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _context;

    public TaskRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task CreateTask(Context.Task groupTask)
    {
        _context.Tasks.Add(groupTask);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateTask(Context.Task groupTask)
    {
        _context.Tasks.Update(groupTask);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task<List<Context.Task>?> GetTasks(int groupId)
        => await _context.Tasks.Include(t => t.Group).Include(u => u.User).Include(s => s.Status).Where(i => i.GroupId == groupId).ToListAsync();

    public async System.Threading.Tasks.Task<Context.Task?> GetTaskById(int id)
        => await _context.Tasks.Include(t => t.Group).Include(u => u.User).Include(s => s.Status).Where(i => i.Id == id).FirstOrDefaultAsync();
    
    public async System.Threading.Tasks.Task<List<Context.Task>?> GetAllTasks()
        => await _context.Tasks.Include(t => t.Group).Include(u => u.User).Include(s => s.Status).ToListAsync();

    public async System.Threading.Tasks.Task RemoveTask(int id)
    {
        var task = await GetTaskById(id);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task<List<Context.Task>?> GetMyTasks(int userId)
        => await _context.Tasks.Include(t => t.Group).Include(u => u.User).Include(s => s.Status).Where(i => i.UserId == userId).ToListAsync();

    public async System.Threading.Tasks.Task<int> GetMaxTaskNumber(int groupId)
        => await _context.Tasks.Include(t => t.Group).Include(u => u.User).Include(s => s.Status).Where(t => t.GroupId == groupId).MaxAsync(t => (int?)t.GroupTaskNumber) ?? 0;

}
