using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUserCompanyRepository _userCompanyRepository;

        public UserCompanyService(IUserCompanyRepository userCompanyRepository)
        {
            _userCompanyRepository = userCompanyRepository;
        }

        public Task<bool> InsertUserCompanyAsync(UserCompany userCompany)
        {
            throw new NotImplementedException();
        }
    }
}