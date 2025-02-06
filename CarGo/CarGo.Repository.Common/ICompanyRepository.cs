using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.Repository.Common
{
    public interface ICompanyRepository
    {
        public Task<CompanyInfoDto?> GetCompanyAsync(Guid id);

        public Task<bool> CreateCompanyAsync(Company company);

        public Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync();

        public Task<bool> NewCompanyLocationAsync(CompanyLocations companyLocations);

        public Task<bool> DeleteCompanyLocationAsync(CompanyLocations companyLocations);

        public Task<bool> UpdateCompanyLocationAsync(CompanyLocations companyLocations);

        public Task<List<CompanyLocationsDto>> GetAllCompanyLocationsAsync();

        public Task<bool> ChangeCompanyIsActiveStatusAsync(Guid Id, bool isActive);
    }
}