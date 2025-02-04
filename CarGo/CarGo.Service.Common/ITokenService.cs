using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ITokenService
    {
        public Task<string> Register(User user);

        public Task<string> Login(string email, string password);
    }
}