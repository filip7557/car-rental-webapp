using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(User user);

        Task<User?> GetUserByIdAsync(Guid id);

        Task<bool> UpdateUserByIdAsync(Guid userId, User user);

        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);

        Task<User?> GetUserByEmailAsync(string email);

        Task<UserDTO?> GetUserDTOByIdAsync(Guid userId);
    }
}