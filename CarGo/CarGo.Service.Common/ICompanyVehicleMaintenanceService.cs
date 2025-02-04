using CarGo.Common;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleMaintenanceService
    {
        public Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance, Guid createdByUserId);

        public Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId, Guid userId);

        public Task<PagedResponse<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId, Paging paging, bool isActiveFilter);
    }
}