using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Avesta.Graph.Test.Src.Controllers;

public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

 
}
