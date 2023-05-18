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

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var user = ((CustomAuthenticationStateProvider) authenticationStateProvider).CachedUser;
        if(user == null)
        {
            navigationManager.NavigateTo($"/Login");
        }
    }
    private void LogOut()
    { 
        ((CustomAuthenticationStateProvider) authenticationStateProvider).Logout();
        navigationManager.NavigateTo("/");
    }
}