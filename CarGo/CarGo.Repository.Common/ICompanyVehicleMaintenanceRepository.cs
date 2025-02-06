using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyVehicleMaintenanceRepository
    {
        public Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance,
            Guid createdByUserId);

        public Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId, Guid userId);

        public Task<List<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId,
            Paging paging, bool isActiveFilter);

        public Task<int> CountAsync(Guid companyVehicleId);
    }
}