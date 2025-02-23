using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IUserCompanyService
    {
        Task<bool> InsertUserCompanyAsync(UserCompany userCompany);
    }
}