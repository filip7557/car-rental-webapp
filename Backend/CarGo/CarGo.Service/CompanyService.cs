using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly ICompanyRequestRepository _companyRequestRepository;
        private readonly ITokenService _tokenService;
        private readonly IManagerService _managerService;

        public CompanyService(ICompanyRepository repository, ITokenService token, IManagerService manager, ICompanyRequestRepository companyRequestRepository)
        {
            _repository = repository;
            _tokenService = token;
            _managerService = manager;
            _companyRequestRepository = companyRequestRepository;
        }

        public async Task<CompanyInfoDto?> GetCompanyAsync(Guid id)
        {
            return await _repository.GetCompanyAsync(id);
        }

        public async Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync()
        {
            return await _repository.GetCompaniesAsync();
        }

        public async Task<bool> NewCompanyLocationAsync(CompanyLocations companyLocations)
        {
            var userId = _tokenService.GetCurrentUserId();
            var role = _tokenService.GetCurrentUserRoleName();
            if (role.Equals(RoleName.Manager.ToString()))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(userId);
                if (!managers.Any(x => x.Id == userId))
                {
                    return false;
                }
            }
            return await _repository.NewCompanyLocationAsync(companyLocations);
        }

        public async Task<bool> DeleteCompanyLocationAsync(CompanyLocations companyLocations)
        {
            var userId = _tokenService.GetCurrentUserId();
            var role = _tokenService.GetCurrentUserRoleName();
            if (role.Equals(RoleName.Manager.ToString()))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(userId);
                if (!managers.Any(x => x.Id == userId))
                {
                    return false;
                }
            }
            return await _repository.DeleteCompanyLocationAsync(companyLocations);
        }

        public async Task<bool> UpdateCompanyLocationAsync(Guid Id, CompanyLocations companyLocations)
        {
            var userId = _tokenService.GetCurrentUserId();
            var role = _tokenService.GetCurrentUserRoleName();
            if (role.Equals(RoleName.Manager.ToString()))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(Id);
                if (!managers.Any(x => x.Id == userId))
                {
                    return false;
                }
            }

            companyLocations.DateUpdated = DateTime.UtcNow;
            companyLocations.UpdatedByUserId = userId;
            return await _repository.UpdateCompanyLocationAsync(companyLocations);
        }

        public async Task<bool> CreateCompanyByAdminAsync(Company company)
        {
            var userId = _tokenService.GetCurrentUserId();
            company.Id = Guid.NewGuid();
            company.CreatedByUserId = userId;
            company.UpdatedByUserId = userId;
            return await _companyRequestRepository.CreateCompanyAsync(company);

        }

        public async Task<List<CompanyLocationsDto>> GetAllCompanyLocationsAsync()
        {
            return await _repository.GetAllCompanyLocationsAsync();
        }

        public async Task<bool> ChangeCompanyIsActiveStatusAsync(Guid Id, bool isActive)
        {
            var userId = _tokenService.GetCurrentUserId();
            var role = _tokenService.GetCurrentUserRoleName();
            if (role.Equals(RoleName.Manager.ToString()))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(Id);
                if (!managers.Any(x => x.Id == userId))
                {
                    return false;
                }
            }

            return await _repository.ChangeCompanyIsActiveStatusAsync(Id, isActive, userId);
        }
    }
}