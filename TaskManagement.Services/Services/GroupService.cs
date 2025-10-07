using AutoMapper;
using TaskManagement.Data;
using TaskManagement.Data.Context;
using TaskManagement.Domain;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _repo;
    private readonly IUserRequestRepository _userReqRepo;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository repo, IUserRequestRepository userReqRepo, IMapper mapper)
    {
        _repo = repo;
        _userReqRepo = userReqRepo;
        _mapper = mapper;
    }

    public async Task CreateGroupAsync(string groupName, int userId)
    {
        var domain = new GroupDomain(groupName, userId);
        var group = await _repo.AddAsync(_mapper.Map<Group>(domain));

        var memberDomain = new GroupMemberDomain(group.Id, userId, "GroupAdmin");
        await _repo.AddGroupMember(_mapper.Map<GroupMember>(memberDomain));
    }

    public async Task<List<GroupDto>> GetAllGroups()
    {
        var groups = await _repo.GetAllAsync();
        return _mapper.Map<List<GroupDto>>(groups);
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
            Id = m.Id,
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

    public async Task<List<GroupDto>> GetNotJoinedGroups(int userId)
    {
        var groups = await _repo.GetGroupsNotJoined(userId);
        return _mapper.Map<List<GroupDto>>(groups);
    }  

    public async Task<List<GroupDto>> GetJoinedGroups(int userId)
    {
        var groups = await _repo.GetGroupsJoined(userId);
        return _mapper.Map<List<GroupDto>>(groups);
    } 

    public async Task RemoveGroupAsync(int id)
    {
        await _repo.RemoveAsync(id);
    }

    public async Task UpdateGroupAsync(int id, string groupName, int userId)
    {
        var domain = new GroupDomain(groupName, userId, id);
        await _repo.UpdateAsync(_mapper.Map<Group>(domain));
    }

    public async Task<List<UserRequestDto>> GetAllUserRequests(int groupId) 
    {
        var userRequests = await _userReqRepo.GetUserRequestsByGroupId(groupId);
        return _mapper.Map<List<UserRequestDto>>(userRequests);
    }

    public async Task AddGroupMember(int groupId, int userId)
    {
        var memberDomain = new GroupMemberDomain(groupId, userId, "GroupUser");
        await _repo.AddGroupMember(_mapper.Map<GroupMember>(memberDomain));
        await RemoveRequest(groupId, userId);          
    }

    public async Task RemoveGroupMember(int id)
    {
        await _repo.RemoveGroupMembers(id);         
    }

    public async Task CreateRequest(int groupId, int userId, string? message)
    {
        var requestDomain = new UserRequestDomain(userId, groupId, message);
        await _userReqRepo.CreateUserRequest(_mapper.Map<UserRequest>(requestDomain));
    }  

    public async Task RemoveRequest(int groupId, int userId) 
    {
        var userReqs = await GetAllUserRequests(groupId);

        if (userReqs != null)
        {
            var id = userReqs.FirstOrDefault(i => i.UserId == userId).Id;
            await _userReqRepo.RemoveUserRequest(id);
        }          
    }
}
