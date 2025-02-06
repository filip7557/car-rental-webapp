using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleMaintenanceService
    {
        public Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance);

        public Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId);

        public Task<PagedResponse<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(
            Guid companyVehicleId, Paging paging);
    }
}