using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Model;
using WebAssemblySEP6.Authentication;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{

    public partial class LogIn
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; }
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
            LoginMessage = "";
            try
            {
                await ((CustomAuthenticationStateProvider) authenticationStateProvider).ValidateLogin(user);
                navigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                LoginMessage = e.Message;
            }
        }
    }
}