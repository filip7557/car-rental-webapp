using CarGo.Common;
using CarGo.Model;

namespace CarGo.Repository.Common
{
    public interface ICompanyVehicleMaintenanceRepository
    {
        public Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance, Guid createdByUserId);

        public Task<List<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId, Paging paging);

        public Task<int> CountAsync(Guid companyVehicleId);
    }
}