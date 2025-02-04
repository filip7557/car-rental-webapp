namespace CarGo.Service.Common
{
    public interface IRoleService
    {
        public Task<Guid> GetDefaultRoleIdAsync();

        public Task<string> GetRoleNameByIdAsync(Guid? roleId);
    }
}