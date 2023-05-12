using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using WebAssemblySEP6.Model;
using WebAssemblySEP6.Communication;
using Microsoft.Extensions.Logging;


namespace WebAssemblySEP6.Pages
{
    public partial class SignUp

    {

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject]private IUserCommunication UserService { get; set; }
        [Inject] private ILogger<SignUp> Logger { get; set; }

        private User user;
        public string LoginMessage { get; set; }
        

        protected override void OnInitialized()
        {
            UserService = new UserCommunication();
            user = new User();
        }
        
        private async Task RegisterUser()
        {
            Logger.LogInformation("In the register user method 1");
            if (await UserService.VerifyEmailAddressAsync(user) == false)
            {
                var returnedUser = await UserService.RegisterUserAsync(user);
                Logger.LogInformation("In the register user method 2");
                if (returnedUser != null && returnedUser.EmailAddress != null)
                {
                    LoginMessage = "Account successfully created";
                    NavigationManager.NavigateTo("/counter");
                }
                else
                {
                    LoginMessage = "Error in account creation";
                }
            }
            else
            {
                LoginMessage = "Email address already in use";
            }
        }
    }
}