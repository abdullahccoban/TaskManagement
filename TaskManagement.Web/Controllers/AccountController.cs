using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;

namespace TaskManagement.Web;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = await _userService.LoginAsync(request.Email, request.Password);
            
            if (token != null) {
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Login successful" });
            } else {
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt_token");
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try 
        {
            var user = await _userService.RegisterAsync(request.Name, request.Email, request.Password);
            return Ok(user);
        } 
        catch (Exception ex) 
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }

}
