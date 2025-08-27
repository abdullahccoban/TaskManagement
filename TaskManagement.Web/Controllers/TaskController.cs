using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Web;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetTasks()
    {
        return Ok(new { message = "JWT ile erişildi!" });
    }

}
