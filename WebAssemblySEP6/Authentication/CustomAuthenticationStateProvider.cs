using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Model;
using Microsoft.JSInterop;
using WebAssemblySEP6.Communication;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebAssemblySEP6.Authentication
{

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly IJSRuntime jsRuntime;
        private readonly IUserCommunication userService;

        public User? CachedUser { get; set; }

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserCommunication userService)
        {
            this.jsRuntime = jsRuntime;
            this.userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (CachedUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User tmp = JsonSerializer.Deserialize<User>(userAsJson);
                    ValidateLogin(tmp);
                }
            }
            else
            {
                identity = SetupClaimsForUser(CachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        public async Task ValidateLogin(User user)
        {
            Console.WriteLine("Validating log in");
            if (string.IsNullOrEmpty(user.EmailAddress)) throw new Exception("Enter email address");
            if (string.IsNullOrEmpty(user.Password)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();

            User userReturned = await userService.LogIn(user); // Use the updated LogIn method

            if (userReturned != null)
            {
                identity = SetupClaimsForUser(userReturned);
                string serialisedData = JsonSerializer.Serialize(userReturned);
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                CachedUser = userReturned;
                Console.WriteLine(userReturned.EmailAddress + "  " + userReturned.Password + "  " + userReturned.UserId);
            }
            else
            {
                throw new Exception("Invalid email or password");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public void Logout()
        {
            CachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity SetupClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            //claims.Add(new Claim("Role", user.Role));
            //claims.Add(new Claim("City", user.City));
            //claims.Add(new Claim("Domain", user.Domain));
            //claims.Add(new Claim("BirthYear", user.BirthYear.ToString()));
            //claims.Add(new Claim("Level", user.SecurityLevel.ToString()));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
    }
}