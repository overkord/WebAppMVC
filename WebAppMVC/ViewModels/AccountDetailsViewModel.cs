using Infrastructures.Entities;

namespace WebAppMVC.ViewModels;

public class AccountDetailsViewModel
{
    public UserEntity User { get; set; } = null!;
    public string Title { get; set; } = "Account Details";

    public ProfileInfoViewModel? ProfileInfo { get; set; } 
    public AccountDetailsBasicInfoViewModel? BasicInfo { get; set; } 
    public AccountDetailsAddressInfoViewModel? AddressInfo { get; set; } 

}
