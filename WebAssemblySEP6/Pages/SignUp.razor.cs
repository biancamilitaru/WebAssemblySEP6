using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{
    public partial class SignUp

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
        
        private async Task RegisterUser()
        {
            if (user.EmailAddress == null || user.Password == null || user.Name == null)
            {
                LoginMessage = "Please complete all fields";
            }
            else
            {
                if (await userService.IsEmailAddressUsed(user) == false)
                {
                    Console.WriteLine("email not used");
                    await userService.AddUserAsync(user);
                    navigationManager.NavigateTo("/home");
                }
                else
                {
                    Console.WriteLine("email used");
                    LoginMessage = "Email address already in use";
                }
            }
        }
    }
}