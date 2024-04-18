using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers;

[Authorize]
public class CoursesController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IActionResult> Index()
    {
      var viewModel = new CoursesIndexViewModel();

        var response = await _httpClient.GetAsync("https://localhost:7115/api/Courses");
        if (response.IsSuccessStatusCode)
        {
            var courses = JsonConvert.DeserializeObject<IEnumerable<CoursesViewModel>>(await response.Content.ReadAsStringAsync());
            if (courses != null && courses.Any())
                viewModel.Courses = courses;
        }

        return View(viewModel);
    }
}
