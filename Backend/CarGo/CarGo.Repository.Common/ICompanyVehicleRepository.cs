using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyVehicleRepository
    {
        Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter);

        Task<List<CompanyVehicle>> GetAllAvailableCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter);

        Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id);

        Task<bool> AddCompanyVehicleAsync(CompanyVehicle companyVehicle, Guid userId);

        Task<bool> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle, Guid userId);

        Task<bool> DeleteCompanyVehicleAsync(Guid compVehId, Guid id);

        Task<int> CountAsync(CompanyVehicleFilter filter, bool available);
    }
}