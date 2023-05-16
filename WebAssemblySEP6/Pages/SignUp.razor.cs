using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{
    public partial class SignUp

    {
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
            if (await userService.IsEmailAddressUsed(user.EmailAddress) == false) 
            {
                Console.WriteLine("email not used");
                //var returnedUser = await userService.AddUserAsync(user);
                //if (returnedUser != null && returnedUser.EmailAddress != null)
                //{
                    //LoginMessage = "Account successfully created";
                    //navigationManager.NavigateTo("/counter");
                //}
                //else
                //{
                    //LoginMessage = "Error in account creation";
                //}
            }
            else
            {
                Console.WriteLine("email used");
                //LoginMessage = "Email address already in use";
            }
        }
    }
}