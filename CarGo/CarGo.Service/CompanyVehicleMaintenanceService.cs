using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyVehicleMaintenanceService : ICompanyVehicleMaintenanceService
    {
        private readonly ICompanyVehicleMaintenanceRepository _companyVehicleMaintenanceRepository;
        private readonly IManagerService _managerService;
        private readonly ICompanyVehicleService _companyVehicleService;
        private readonly ITokenService _tokenService;

        public CompanyVehicleMaintenanceService(
            ICompanyVehicleMaintenanceRepository companyVehicleMaintenanceRepository, IManagerService managerService, ICompanyVehicleService companyVehicleService, ITokenService tokenService)
        {
            _companyVehicleMaintenanceRepository = companyVehicleMaintenanceRepository;
            _managerService = managerService;
            _companyVehicleService = companyVehicleService;
            _tokenService = tokenService;
        }

        private async Task<CompanyVehicleMaintenance?> GetCompanyVehicleMaintenanceByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await _companyVehicleMaintenanceRepository.GetCompanyVehicleMaintenanceByIdAsync(id);
        }

        public async Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance)
        {
            var createdByUserId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(maintenance.CompanyVehicleId);
                var managers = await _managerService.GetAllCompanyManagersAsync(companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == createdByUserId))
                    return false;
            }
            return await _companyVehicleMaintenanceRepository.SaveCompanyVehicleMaintenanceAsync(maintenance,
                createdByUserId);
        }

        public async Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId)
        {
            var maintenance = await GetCompanyVehicleMaintenanceByIdAsync(maintenanceId);
            if (maintenance == null) return false;
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(maintenance.CompanyVehicleId);
                var managers = await _managerService.GetAllCompanyManagersAsync(companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return false;
            }
            return await _companyVehicleMaintenanceRepository.DeleteCompanyVehicleMaintenanceByIdAsync(maintenanceId,
                userId);
        }

        public async Task<PagedResponse<CompanyVehicleMaintenance>?> GetMaintenancesByCompanyVehicleIdAsync(
            Guid companyVehicleId, Paging paging)
        {
            var userId = _tokenService.GetCurrentUserId();
            var role = _tokenService.GetCurrentUserRoleName();
            var isActiveFilter = role.Equals(RoleName.Administrator);
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(companyVehicleId);
                var managers = await _managerService.GetAllCompanyManagersAsync(companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return null;
            }
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