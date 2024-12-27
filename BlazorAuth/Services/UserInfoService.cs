using BlazorAuth.Models;

namespace BlazorAuth.Services
{
    public class UserInfoService
    {
        private readonly List<UserInfo> _users;

        public UserInfoService(List<UserInfo> users)
        {
            _users = users;
            _users.Add(new UserInfo()
            {
                UserName = "admin",
                Password = "admin"
            });

            _users.Add(new UserInfo()
            {
                UserName = "user",
                Password = "user"
            });
        }

        public UserInfo? GetByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
