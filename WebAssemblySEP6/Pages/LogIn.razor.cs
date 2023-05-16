using Microsoft.AspNetCore.Components;
using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages;

public partial class LogIn
{
    [Inject]
    private NavigationManager navigationManager { get; set; }
    private IUserCommunication userService { get; set; }
    private User user;
    public string LoginMessage { get; set; }
        
    protected override void OnInitialized()
    {
        userService = new UserCommunication();
        user = new User();
    }
        
    private async Task UserLogIn()
    {
        if (user.EmailAddress == null || user.Password == null)
        {
            LoginMessage = "Please complete all fields";
        }
        else
        {
            if (await userService.IsEmailAddressUsed(user) == false)
            {
                Console.WriteLine("email not used");
                LoginMessage = "No user found with this email";
            }
            else
            {
                Console.WriteLine("email used - verifying log in credentials");
                if (await userService.LogIn(user))
                {
                    Console.WriteLine("logging in success");
                    navigationManager.NavigateTo("/counter");
                }
                else
                {
                    Console.WriteLine("logging in failed");
                    LoginMessage = "Incorrect password";
                }

            }
        }
    }
}