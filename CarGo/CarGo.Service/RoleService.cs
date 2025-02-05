using CarGo.Repository.Common;
using CarGo.Service.Common;
using CarGo.Model;

namespace CarGo.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Guid> GetDefaultRoleIdAsync()
        {
            return _roleRepository.GetDefaultRoleIdAsync();
        }

        public Task<string> GetRoleNameByIdAsync(Guid? roleId)
        {
            return _roleRepository.GetRoleNameByIdAsync((Guid)roleId!)!;
        }

        public async Task<List<Role>?> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }
    }
}