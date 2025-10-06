using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Data;

public class StatusRepository : IStatusRepository
{
    private readonly TaskManagementDbContext _context;

    public StatusRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<List<Status>?> GetStatuses(int groupId) => await _context.Statuses.Where(i => i.GroupId == groupId).ToListAsync();

    public async Task<Status?> GetStatusById(int id) => await _context.Statuses.FindAsync(id);

    public async Task CreateStatus(Status status)
    {
        _context.Statuses.Add(status);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveStatus(int id)
    {
        var status = await GetStatusById(id);

        if (status != null)
        {
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }        
    }

    public async Task UpdateStatus(Status status)
    {
        _context.Statuses.Update(status);
        await _context.SaveChangesAsync();
    }
}
