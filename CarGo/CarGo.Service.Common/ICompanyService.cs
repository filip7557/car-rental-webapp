using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.Service.Common
{
    public interface ICompanyService
    {
        Task<CompanyInfoDto?> GetCompanyAsync(Guid id);

        Task<bool> CreateCompanyByAdminAsync(Company company);

        Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync();

        Task<bool> NewCompanyLocationAsync(CompanyLocations companyLocations);

        Task<bool> DeleteCompanyLocationAsync(CompanyLocations companyLocations);

        Task<bool> UpdateCompanyLocationAsync(Guid Id, CompanyLocations companyLocations);

        Task<List<CompanyLocationsDto>> GetAllCompanyLocationsAsync();

        public Task<bool> ChangeCompanyIsActiveStatusAsync(Guid Id, bool isActive);
    }
}