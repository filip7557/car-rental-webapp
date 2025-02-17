using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleService
    {
        Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter);

        Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id);

        Task<bool> AddCompanyVehicleAsync(CompanyVehicle companyVehicle);

        Task<bool> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle);

        Task<bool> DeleteCompanyVehicleAsync(Guid compVehId, Guid id);
    }
}