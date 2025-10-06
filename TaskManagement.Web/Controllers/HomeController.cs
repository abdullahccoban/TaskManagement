using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGroupService _groupService;

    public HomeController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [JwtAuthorize]
    public async Task<IActionResult> Index()
    {
        var groups = await _groupService.GetAllGroups();
        
        var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
        var myGroups = await _groupService.GetMyGroups(userId);

        GroupViewModel viewModel = new GroupViewModel();
        viewModel.Groups = groups;
        viewModel.MyGroups = myGroups;
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
