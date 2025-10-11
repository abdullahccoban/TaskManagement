using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagement.Web;

public class JwtAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var token = context.HttpContext.Request.Cookies["jwt_token"];
            if (string.IsNullOrEmpty(token))
            {
                if (!context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }
                
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var username = jwt.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
                var email = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var userId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                var role = jwt.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
                context.HttpContext.Items["Username"] = username;
                context.HttpContext.Items["Email"] = email;
                context.HttpContext.Items["UserId"] = userId;
                context.HttpContext.Items["Role"] = role;
            }
            catch
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            await next();
    }
}
