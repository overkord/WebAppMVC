using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class HomeController : Controller
{

    // https://domain.com/Index
    public IActionResult Index()
    {
        ViewData["Title"] = "Ultimate Task Management Assistant";

        return View();

    }

    [Route("/error")]

    public IActionResult Error404(int statusCode) => View();
}
