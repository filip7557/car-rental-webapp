using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleMaintenanceService
    {
        public Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance, Guid createdByUserId);
    }
}
