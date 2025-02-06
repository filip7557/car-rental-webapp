using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private readonly ICompanyRequestRepository _companyRequestRepository;

        private readonly ITokenService _tokenService;

        public CompanyRequestService(ICompanyRequestRepository companyRequestRepository, ICompanyService companyService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _companyRequestRepository = companyRequestRepository;
        }

        public async Task<bool> UpdateCompanyRequestAsync(CompanyRequest companyRequest)
        {
            var userId = _tokenService.GetCurrentUserId();
            companyRequest.UpdatedByUserId = userId;
            return await _companyRequestRepository.UpdateCompanyRequestAsync(companyRequest);
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            var userId = _tokenService.GetCurrentUserId();
            company.Id = Guid.NewGuid();
            company.CreatedByUserId = userId;
            company.UpdatedByUserId = userId;
            return await _companyRequestRepository.CreateCompanyAsync(company);
        }

        public async Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest)
        {
            return await _companyRequestRepository.NewCompanyRequest(newCompanyRequest);
        }
    }
}