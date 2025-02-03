using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
namespace CarGo.Service
{
    public class CompanyVehicleMaintenanceService : ICompanyVehicleMaintenanceService
    {
        private readonly ICompanyVehicleMaintenanceRepository _companyVehicleMaintenanceRepository;
        public CompanyVehicleMaintenanceService(ICompanyVehicleMaintenanceRepository companyVehicleMaintenanceRepository)
        {
            _companyVehicleMaintenanceRepository = companyVehicleMaintenanceRepository;
        }
        public async Task<bool> SaveCompanyVehicleMaintenance(CompanyVehicleMaintenance maintenance, Guid createdByUserId)
        {
            return await _companyVehicleMaintenanceRepository.SaveCompanyVehicleMaintenance(maintenance, createdByUserId);
        }
    }
}
