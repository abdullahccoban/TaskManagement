using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;

namespace TaskManagement.Web;

public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IStatusService _statusService;
    private readonly IGroupService _groupService;

    public TaskController(ITaskService taskService, IStatusService statusService, IGroupService groupService)
    {
        _taskService = taskService;
        _statusService = statusService;
        _groupService = groupService;
    }

    [JwtAuthorize]
    [GroupAuthorize(["GroupAdmin", "GroupUser"])]
    [Route("Task/Index/{groupId:int}")]
    public async Task<IActionResult> Index(int groupId)
    {
        var tasks = await _taskService.GetGroupTasksAsync(groupId);
        var statuses = await _statusService.GetStatuses(groupId);
        var groupInformation = await _groupService.GetGroupDetail(groupId);
        var groupMembers = await _groupService.GetGroupMembers(groupId);

        var viewModel = new TaskViewModel {
            GroupTasks = tasks,
            GroupDetails = groupInformation,
            GroupMembers = groupMembers,
            Statuses = statuses
        };

        return View(viewModel);
    }

    [JwtAuthorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto createTask) 
    {
        try
        {
            await _taskService.CreateTaskAsync(createTask);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
