using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class AuthController : Controller
{
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }
}
