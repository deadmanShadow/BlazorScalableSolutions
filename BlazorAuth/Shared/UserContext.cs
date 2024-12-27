using BlazorAuth.AppSettings;
using BlazorAuth.Models;
using BlazorAuth.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;

namespace BlazorAuth.Shared
{
    public class UserContext : ComponentBase
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private ProtectedLocalStorage ProtectedLocalStorage { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private UserInfoService UserInfoService { get; set; } = default!;

        private CustomAuthStateProvider _authProvider = default!;
        public UserInfo User => _authProvider.User;

        protected override void OnInitialized()
        {
            _authProvider = (CustomAuthStateProvider)AuthenticationStateProvider;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_authProvider.IsAuthenticated)
            {
                await UserReAuthorizeAsync();
            }

            if (_authProvider.IsAuthenticated)
            {
                await OnAuthenticatedAsync(firstRender);
            }
        }

        public virtual Task OnAuthenticatedAsync(bool firstRender)
        {
            return Task.CompletedTask;
        }

        public async Task WriteToLocalStorageAsync(string key, UserInfo user)
        {
            await ProtectedLocalStorage.SetAsync(key, user);
        }

        public async Task<UserInfo> ReadFromLocalStorageAsync(string key)
        {
            var result = await ProtectedLocalStorage.GetAsync<UserInfo>(key);
            return result.Success ? result.Value : null;
        }

        public async Task RemoveFromLocalStorageAsync(string key)
        {
            await ProtectedLocalStorage.DeleteAsync(key);
        }

        public async Task<bool> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = UserInfoService.GetByUserName(username);
            if (user == null || user.Password != password)
            {
                return false;
            }

            await WriteToLocalStorageAsync("UserInfo", user);
            await _authProvider.SetUser(user);
            return true;
        }

        public async Task Logout()
        {
            await RemoveFromLocalStorageAsync("UserInfo");
            await _authProvider.SetUser(new UserInfo());
        }

        public async Task<bool> UserReAuthorizeAsync()
        {
            var user = await ReadFromLocalStorageAsync("UserInfo");
            if (user == null)
            {
                return false;
            }

            await _authProvider.SetUser(user);
            return true;
        }

        public void NavTo(string path, bool refresh = false)
        {
            try
            {
                NavigationManager.NavigateTo(path, refresh);
            }
            catch { }

        }
    }
}
