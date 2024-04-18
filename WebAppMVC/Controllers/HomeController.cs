using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers;

public class HomeController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;



    // https://domain.com/Index
    public IActionResult Index()
    {
        ViewData["Title"] = "Ultimate Task Management Assistant";

        return View();

    }


    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7115/api/subscribe", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["StatusMessage"] = "You are now subscribed!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["StatusMessage"] = "You are already subscribed!";
            }

        }
        else 
        {
            TempData["StatusMessage"] = "Invalid email address";
        }
        return RedirectToAction("Index", "Home", "subscribe");
    }

    [Route("/error")]

    public IActionResult Error404(int statusCode) => View();
}
