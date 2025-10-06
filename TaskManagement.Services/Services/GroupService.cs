using AutoMapper;
using TaskManagement.Data;
using TaskManagement.Data.Context;
using TaskManagement.Domain;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _repo;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateGroupAsync(string groupName, int userId)
    {
        var domain = new GroupDomain(groupName, userId);
        var group = await _repo.AddAsync(_mapper.Map<Group>(domain));

        var memberDomain = new GroupMemberDomain(group.Id, userId, "GroupAdmin");
        await _repo.AddGroupMember(_mapper.Map<GroupMember>(memberDomain));
    }

    public async Task<GroupDto> GetGroupDetail(int id)
    {
        var detail = await _repo.GetByIdAsync(id);
        return _mapper.Map<GroupDto>(detail);
    }

    public async Task<List<GroupMemberDto>> GetGroupMembers(int groupId)
    {
        var groupMembers = await _repo.GetGroupMembers(groupId);

        return groupMembers.Select(m => new GroupMemberDto
        {
            Id = m.User.Id,
            Username = m.User.Username,
            Email = m.User.Email,
            Role = m.Role
        }).ToList();
    }

    public async Task<List<GroupDto>> GetMyGroups(int userId)
    {
        var groups = await _repo.GetMyGroupsAsync(userId);
        return _mapper.Map<List<GroupDto>>(groups);
    }
}
