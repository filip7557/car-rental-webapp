using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyVehicleMaintenanceService : ICompanyVehicleMaintenanceService
    {
        private readonly ICompanyVehicleMaintenanceRepository _companyVehicleMaintenanceRepository;
        private readonly ITokenService _tokenService;

        public CompanyVehicleMaintenanceService(
            ICompanyVehicleMaintenanceRepository companyVehicleMaintenanceRepository, ITokenService tokenService)
        {
            _companyVehicleMaintenanceRepository = companyVehicleMaintenanceRepository;
            _tokenService = tokenService;
        }

        public async Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance)
        {
            var createdByUserId = _tokenService.GetCurrentUserId();
            return await _companyVehicleMaintenanceRepository.SaveCompanyVehicleMaintenanceAsync(maintenance,
                createdByUserId);
        }

        public async Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId)
        {
            var userId = _tokenService.GetCurrentUserId();
            return await _companyVehicleMaintenanceRepository.DeleteCompanyVehicleMaintenanceByIdAsync(maintenanceId,
                userId);
        }

        public async Task<PagedResponse<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(
            Guid companyVehicleId, Paging paging)
        {
            var role = _tokenService.GetCurrentUserRoleName();
            var isActiveFilter = role.Equals(RoleName.Administrator);
            var maintenances =
                await _companyVehicleMaintenanceRepository.GetMaintenancesByCompanyVehicleIdAsync(companyVehicleId,
                    paging, isActiveFilter);
            return new PagedResponse<CompanyVehicleMaintenance>
            {
                Data = maintenances,
                PageNumber = paging.PageNumber,
                PageSize = paging.Rpp,
                TotalRecords = await _companyVehicleMaintenanceRepository.CountAsync(companyVehicleId),
            };
        }
    }
}