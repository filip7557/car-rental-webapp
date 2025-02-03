using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ICompanyVehicleMaintenanceService
    {
        public Task<bool> SaveCompanyVehicleMaintenance(CompanyVehicleMaintenance maintenance, Guid createdByUserId);
    }
}
