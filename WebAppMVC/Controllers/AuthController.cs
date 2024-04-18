using Infrastructures.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers;


//[Authorize]

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;


    #region Sign Up
    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Index", "Profile");
        }

        var viewModel = new SignUpViewModel();
        ViewData["Title"] = viewModel.Title;
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
                ModelState.AddModelError("AlreadyExists", "User with the same email already exists");
                ViewData["ErrorMessage"] = "User with the same email already exists";
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
    #endregion


    #region Sign In
    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn(string returnUrl)
    {
        var viewModel = new SignInViewModel();
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index", "Profile");
        ViewData["ReturnUrl"] = returnUrl == Url.Content("~/");
        return View(viewModel);

    }
    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Details", "Account");
            }
        }

        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }
    #endregion


    #region Sign Out
    [Route("/signout")]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion


}
