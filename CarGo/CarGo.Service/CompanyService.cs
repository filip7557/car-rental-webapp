using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyInfoDto?> GetCompanyAsync(Guid id)
        {
            return await _repository.GetCompanyAsync(id);
        }

        public async Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync()
        {
            return await _repository.GetCompaniesAsync();
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            return await _repository.CreateCompanyAsync(company);
        }

        public async Task<bool> NewCompanyLocationAsync(CompanyLocations companyLocations)
        {
            return await _repository.NewCompanyLocationAsync(companyLocations);
        }

        public Task<bool> DeleteCompanyLocationAsync(CompanyLocations companyLocations)
        {
            return _repository.DeleteCompanyLocationAsync(companyLocations);
        }

        public Task<bool> UpdateCompanyLocationAsync(CompanyLocations companyLocations)
        {
            return _repository.UpdateCompanyLocationAsync(companyLocations);
        }

        public Task<bool> CreateCompanyByAdminAsync(Company company)
        {
            company.Id = Guid.NewGuid();
            return _repository.CreateCompanyAsync(company);
        }

        public Task<List<CompanyLocationsDto>> GetAllCompanyLocationsAsync()
        {
            return _repository.GetAllCompanyLocationsAsync();
        }

        public Task<bool> ChangeCompanyIsActiveStatusAsync(Guid Id, bool isActive)
        {
            return _repository.ChangeCompanyIsActiveStatusAsync(Id, isActive);
        }
    }
}