using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Web;

public class GroupAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    private readonly string[] _roles;
    public GroupAuthorizeAttribute(params string[] roles) => _roles = roles;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var http = context.HttpContext;
        var db = http.RequestServices.GetRequiredService<TaskManagementDbContext>();
        var userId = int.Parse(http.Items["UserId"]?.ToString() ?? "0");


        if (!context.ActionArguments.TryGetValue("groupId", out var groupIdObj) || groupIdObj is not int groupId)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }

        var member = await db.GroupMembers
            .FirstOrDefaultAsync(m => m.GroupId == groupId && m.UserId == userId);

        if (member == null || !_roles.Contains(member.Role, StringComparer.OrdinalIgnoreCase))
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }

        context.HttpContext.Items["GroupRole"] = member.Role;
        await next();
    }
}
