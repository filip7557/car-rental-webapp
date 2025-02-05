using CarGo.Model;

﻿namespace CarGo.Service.Common
{
    public interface IRoleService
    {
        public Task<Guid> GetDefaultRoleIdAsync();

        public Task<string> GetRoleNameByIdAsync(Guid? roleId);

        public Task<List<Role>?> GetAllAsync();
        
        public Task<Role?> GetByIdAsync(Guid id);
    }
}