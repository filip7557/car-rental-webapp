using CarGo.Repository.Common;
using CarGo.Service.Common;

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
    }
}