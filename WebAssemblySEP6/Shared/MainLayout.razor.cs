using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebAssemblySEP6.Authentication;

namespace WebAssemblySEP6.Shared;

public partial class MainLayout
{
    [Inject]
    private NavigationManager navigationManager { get; set; }
    [CascadingParameter] 
    protected Task<AuthenticationState> AuthStat { get; set; }
    [Inject]
    private AuthenticationStateProvider authenticationStateProvider { get; set; }

    private void LogOut()
    { 
        ((CustomAuthenticationStateProvider) authenticationStateProvider).Logout();
        navigationManager.NavigateTo("/");
    }

    private void LogIn()
    {
        navigationManager.NavigateTo("/login");
    }
}