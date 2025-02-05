using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository repository, IRoleService roleService)
        {
            _userRepository = repository;
            _roleService = roleService;
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

        public async Task<bool> UpdateUserByIdAsync(Guid userId, UserDTO newUser)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await _userRepository.UpdateUserByIdAsync(userId, newUser);
        }

        public async Task<bool> UpdateUserRoleByUserIdAsync(Guid userId, Guid roleId)
        {
            if (roleId == Guid.Empty)
                return false;
            return await _userRepository.UpdateUserRoleByUserIdAsync(userId, roleId);
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<UserResponse?> GetUserDTOByIdAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                return null;

            return new UserResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = await _roleService.GetRoleNameByIdAsync(user.RoleId),
            };
        }
    }
}