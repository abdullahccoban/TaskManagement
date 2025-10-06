namespace TaskManagement.Services;

public interface IStatusService
{
    Task<List<StatusDto>?> GetStatuses(int groupId);
    Task<StatusDto?> GetStatusByIdAsync(int id);
    Task CreateStatusAsync(string status, int groupId);
    Task UpdateStatusAsync(int id, string status, int groupId);
    Task RemoveStatusAsync(int id);
}
