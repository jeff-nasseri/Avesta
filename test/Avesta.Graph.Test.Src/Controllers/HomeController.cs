using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Avesta.Graph.Test.Src.Controllers;

public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{

    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return Content("Click <a href = '/avesta/graph'>here</a> to test the avesta graph functionality ! <a href='/avesta/graph/graph.json'>json</a>", "text/html; charset=UTF-8");
    }

 
}
