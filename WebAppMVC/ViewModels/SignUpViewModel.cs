using WebAppMVC.Models;

namespace WebAppMVC.ViewModels;

public class SignUpViewModel
{
   public string Title { get; set; } = "Sign Up";
   public SignUpModel Form { get; set; } = new SignUpModel();

    public bool TermsAndCondition { get; set; } = false;
}
