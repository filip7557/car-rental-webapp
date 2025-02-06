using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyRequestRepository _companyRequestRepository;
        private readonly IUserCompanyService _userCompanyService;

        public CompanyRequestService(ICompanyRequestRepository companyRequestRepository, ICompanyService companyService,
            IUserCompanyService userCompanyService) // Add the missing parameter
        {
            _companyService = companyService;
            _companyRequestRepository = companyRequestRepository;
            _userCompanyService = userCompanyService;
        }

        public async Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest)
        {
            return await _companyRequestRepository.NewCompanyRequest(newCompanyRequest);
        }
    }
}