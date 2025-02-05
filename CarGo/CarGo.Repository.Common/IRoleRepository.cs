namespace CarGo.Repository.Common
{
    public interface IRoleRepository
    {
        public Task<Guid> GetDefaultRoleIdAsync();

        public Task<string> GetRoleNameByIdAsync(Guid roleId);
        
        public Task<List<Role>> GetAllAsync();

        public Task<Role?> GetByIdAsync(Guid id);
    }
}