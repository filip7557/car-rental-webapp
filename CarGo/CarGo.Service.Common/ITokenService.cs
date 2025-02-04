using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ITokenService
    {
        public Task<bool> RegisterAsync(UserDTO user);

        public Task<string> LoginAsync(string email, string password);
    }
}