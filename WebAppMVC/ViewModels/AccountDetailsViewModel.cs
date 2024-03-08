using WebAppMVC.Models;

namespace WebAppMVC.ViewModels;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
    {
        ProfileImage = "~/images/profile-image.svg",
        FirstName = "Albin",
        LastName = "Larsson", 
        Email = "Albin-.-@hotmail.com" 
    };
    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();

}
