using CarGo.Common;
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
        public async Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance, Guid createdByUserId)
        {
            return await _companyVehicleMaintenanceRepository.SaveCompanyVehicleMaintenanceAsync(maintenance, createdByUserId);
        }
        public async Task<PagedResponse<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId, Paging paging)
        {
            var maintenances = await _companyVehicleMaintenanceRepository.GetMaintenancesByCompanyVehicleIdAsync(companyVehicleId, paging);
            return new PagedResponse<CompanyVehicleMaintenance>
            {
                Data = maintenances,
                PageNumber = paging.PageNumber,
                PageSize = paging.Rpp,
                TotalRecords = 0 // TODO: Add count
            };
        }
    }
}
