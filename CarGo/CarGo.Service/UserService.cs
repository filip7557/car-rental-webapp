using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            // TODO: Fetch user role Id from database.
            return await _userRepository.RegisterUserAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> UpdateUserByIdAsync(Guid userId, User newUser)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await _userRepository.UpdateUserByIdAsync(userId, newUser);
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _userRepository.GetUSerByEmailAsync(email);
        }
    }
}