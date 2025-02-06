using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public ManagerService(IManagerRepository managerRepository, IUserService userService, IRoleService roleService)
        {
            _managerRepository = managerRepository;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<List<User>> GetAllCompanyManagersAsync(Guid companyId)
        {
            return await _managerRepository.GetAllCompanyManagersAsync(companyId);
        }

        public async Task<User?> GetManagerByIdAsync(Guid userId)
        {
            return await _managerRepository.GetManagerByIdAsync(userId);
        }

        public async Task<bool> AddManagerToCompanyAsync(Guid companyId, User user)
        {
            var result = await _managerRepository.AddManagerToCompanyAsync(companyId, user.Id);
            if (result)
            {
                var role = await _roleService.GetRoleByNameAsync("Manager");
                return await _userService.UpdateUserRoleByUserIdAsync(user.Id, role.Id);
            }

            return false;
        }

        public async Task<bool> RemoveManagerFromCompanyAsync(Guid companyId, User user)
        {
            var result = await _managerRepository.RemoveManagerFromCompanyAsync(companyId, user.Id);
            if (result)
            {
                var roleId = await _roleService.GetDefaultRoleIdAsync();
                return await _userService.UpdateUserRoleByUserIdAsync(user.Id, roleId);
            }

            return false;
        }
    }
}