using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(User user);

        Task<User?> GetUserByIdAsync(Guid id);

        Task<bool> UpdateUserByIdAsync(Guid id, UserDTO user);

        public Task<bool> UpdateUserRoleByUserIdAsync(Guid id, User user);

        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);

        Task<User?> GetUserByEmailAsync(string email);
    }
}