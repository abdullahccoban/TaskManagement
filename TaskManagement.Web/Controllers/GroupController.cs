using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;

namespace TaskManagement.Web;

public class GroupController : Controller
{
    private readonly IGroupService _groupService;
    private readonly IStatusService _statusService;

    public GroupController(IGroupService groupService, IStatusService statusService)
    {
        _groupService = groupService;
        _statusService = statusService;
    }

    [JwtAuthorize]
    public async Task<IActionResult> Index()
    {
        var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
        var myGroups = await _groupService.GetMyGroups(userId);

        GroupViewModel viewModel = new GroupViewModel();
        viewModel.MyGroups = myGroups;
        return View(viewModel);
    }

    [JwtAuthorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GroupDto group) 
    {
        try
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            await _groupService.CreateGroupAsync(group.GroupName, userId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [JwtAuthorize]
    public async Task<IActionResult> Detail(int id)
    {
        var groupDetail = await _groupService.GetGroupDetail(id);
        var members = await _groupService.GetGroupMembers(id);
        var statuses = await _statusService.GetStatuses(id);
        GroupViewModel viewModel = new GroupViewModel();
        viewModel.GroupDetail = groupDetail;
        viewModel.GroupMembers = members;
        viewModel.Statuses = statuses;
        return View(viewModel);
    }
}
