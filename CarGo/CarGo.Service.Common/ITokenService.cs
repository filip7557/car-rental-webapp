using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ITokenService
    {
        public Task<bool> RegisterAsync(User user);

        public Task<string> LoginAsync(string email, string password);
    }
}