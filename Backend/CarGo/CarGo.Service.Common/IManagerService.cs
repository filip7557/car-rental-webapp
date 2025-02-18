using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IManagerService
    {
        Task<User?> GetManagerByIdAsync(Guid userId);

        Task<List<User>> GetAllCompanyManagersAsync(Guid companyId);

        Task<bool> AddManagerToCompanyAsync(Guid companyId, User user);

        Task<bool> RemoveManagerFromCompanyAsync(Guid companyId, User user);

        Task<Guid> GetCompanyIdByUserIdAsync(Guid userId);
    }
}