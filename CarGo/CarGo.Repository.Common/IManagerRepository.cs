using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface IManagerRepository
    {
        Task<User?> GetManagerByIdAsync(Guid userId);

        Task<List<User>> GetAllCompanyManagersAsync(Guid companyId);

        Task<bool> AddManagerToCompanyAsync(Guid companyId, Guid managerId);

        Task<bool> RemoveManagerFromCompanyAsync(Guid companyId, Guid managerId);
    }
}