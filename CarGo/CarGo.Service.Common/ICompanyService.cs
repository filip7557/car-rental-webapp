using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyService
    {
        Task<CompanyInfoDto?> GetCompanyAsync(Guid id);

        Task<bool> CreateCompanyAsync(Company company);

        Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync();
    }
}