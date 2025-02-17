using CarGo.Model;
using CarGo.Common;

namespace CarGo.Service.Common
{
    public interface ITokenService
    {
        public Task<bool> RegisterAsync(UserDTO user);

        public Task<LoginResponse> LoginAsync(string email, string password);

        public Guid GetCurrentUserId();

        public string GetCurrentUserRoleName();
    }
}