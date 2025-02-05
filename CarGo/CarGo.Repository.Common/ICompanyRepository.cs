using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyRepository
    {
        public Task<CompanyInfoDto?> GetCompanyAsync(Guid id);

        public Task<bool> CreateCompanyAsync(Company company);

        public Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync();

        public Task<bool> NewCompanyLocation(CompanyLocations companyLocations);
    }
}