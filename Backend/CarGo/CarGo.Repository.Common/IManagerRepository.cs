using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IManagerRepository
    {
        Task<User?> GetManagerByIdAsync(Guid userId);

        Task<List<User>> GetAllCompanyManagersAsync(Guid companyId);

        Task<bool> AddManagerToCompanyAsync(Guid companyId, User newManager);

        Task<bool> RemoveManagerFromCompanyAsync(Guid companyId, Guid managerId);

        Task<Guid> GetCompanyIdByUserIdAsync(Guid userId);
    }
}