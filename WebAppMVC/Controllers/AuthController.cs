using Infrastructures.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if (exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with the sam email address alaready exists");
                ViewData["ErrorMessage"] = "User with the same email address already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");

            }
        }
            return View(viewModel);
    }

  
    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }



    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }
        }

        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
        

        // var result = _authService.SignIn(viewModel.Form);
        // if (result)
           // return RedirectToAction("SignIn", "Auth");

        viewModel.ErrorMessage = "Incorrect email or password";
        return View(viewModel);

    }
}
