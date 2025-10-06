using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;

namespace TaskManagement.Web;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpPost("CreateStatus")]
    public async Task<IActionResult> CreateStatus([FromBody] StatusDto status)
    {
        try
        {
            await _statusService.CreateStatusAsync(status.Status, status.GroupId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("UpdateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] StatusDto status)
    {
        try
        {
            await _statusService.UpdateStatusAsync(status.Id, status.Status, status.GroupId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("DeleteStatus")]
    public async Task<IActionResult> DeleteStatus([FromBody] int id)
    {
        try
        {
            await _statusService.RemoveStatusAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
