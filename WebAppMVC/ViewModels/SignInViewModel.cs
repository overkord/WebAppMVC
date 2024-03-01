using WebAppMVC.Models;

namespace WebAppMVC.ViewModels;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign In";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessaage { get; set; }

}
