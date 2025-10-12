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
        var tasks = await _taskService.GetGroupTasksAsync(groupId, 0, 10);
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
    [GroupAuthorize(["GroupAdmin", "GroupUser"])]
    [Route("Task/Board/{groupId:int}")]
    public async Task<IActionResult> Board(int groupId)
    {
        var tasks = await _taskService.GetGroupTasksAsync(groupId, 0, 0, false);
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

    [JwtAuthorize]
    [HttpPost]
    public async Task<IActionResult> SoftDelete([FromBody] int id) 
    {
        try
        {
            await _taskService.SoftDeleteTaskAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [JwtAuthorize]
    [HttpPost]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusDto updateTaskStatus) 
    {
        try
        {
            await _taskService.UpdateTaskStatusAsync(updateTaskStatus.Id, updateTaskStatus.StatusId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [JwtAuthorize]
    [HttpPost]
    public async Task<IActionResult> UpdateTaskUser([FromBody] UpdateTaskUserDto updateTaskUser) 
    {
        try
        {
            await _taskService.UpdateTaskUserAsync(updateTaskUser.Id, updateTaskUser.UserId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [JwtAuthorize]
    [HttpGet]
    public async Task<IActionResult> GetTasksLoadMore(int groupId, int skip, int take) 
    {
        try
        {
            var result = await _taskService.GetGroupTasksAsync(groupId, skip, take);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }




}
