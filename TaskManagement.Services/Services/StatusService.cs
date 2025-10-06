
using AutoMapper;
using TaskManagement.Data;
using TaskManagement.Data.Context;
using TaskManagement.Domain;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _repo;
    private readonly IMapper _mapper;

    public StatusService(IStatusRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateStatusAsync(string status, int groupId)
    {
        var domain = new StatusDomain(status, groupId);
        await _repo.CreateStatus(_mapper.Map<Status>(domain));
    }

    public async Task<StatusDto?> GetStatusByIdAsync(int id)
    {
        var status = await _repo.GetStatusById(id);
        return _mapper.Map<StatusDto>(status);
    }

    public async Task<List<StatusDto>?> GetStatuses(int groupId)
    {
        var statuses = await _repo.GetStatuses(groupId);
        return _mapper.Map<List<StatusDto>>(statuses);
    }

    public async Task RemoveStatusAsync(int id)
    {
        await _repo.RemoveStatus(id);
    }

    public async Task UpdateStatusAsync(int id, string status, int groupId)
    {
        var domain = new StatusDomain(status, groupId, id);
        await _repo.UpdateStatus(_mapper.Map<Status>(domain));
    }
}
