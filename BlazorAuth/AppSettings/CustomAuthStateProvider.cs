using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using BlazorAuth.Models;

namespace BlazorAuth.AppSettings
{
    public class CustomAuthStateProvider(ProtectedLocalStorage localStorage) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public UserInfo User { get; private set; } = new();
        public bool IsAuthenticated => !string.IsNullOrEmpty(User.UserName);

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var result = await localStorage.GetAsync<UserInfo>("UserInfo");
                User = result.Success ? result.Value : new UserInfo();

                var identity = IsAuthenticated ? CreateClaimsIdentity(User) : new ClaimsIdentity();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task SetUser(UserInfo user)
        {
            User = user;
            if (!string.IsNullOrEmpty(user.UserName))
            {
                await localStorage.SetAsync("UserInfo", user);
            }
            else
            {
                await localStorage.DeleteAsync("UserInfo");
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserInfo user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName)
            };

            return new ClaimsIdentity(claims, "CustomAuth");
        }
    }
}
