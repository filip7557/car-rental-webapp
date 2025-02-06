using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyVehicleService : ICompanyVehicleService
    {
        private ICompanyVehicleRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IManagerService _managerService;

        public CompanyVehicleService(ICompanyVehicleRepository repository, ITokenService tokenService, IManagerService managerService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _managerService = managerService;
        }

        public async Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter)
        {
            return await _repository.GetAllCompanyVehiclesAsync(sorting, paging, filter);
        }

        public async Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id)
        {
            return await _repository.GetCompanyVehicleByIdAsync(id);
        }

        public async Task<bool> AddCompanyVehicleAsync(CompanyVehicle companyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return false;
            }
            return await _repository.AddCompanyVehicleAsync(companyVehicle, userId);
        }

        public async Task<bool> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync(updatedCompanyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return false;
            }
            return await _repository.UpdateCompanyVehicleAsync(id, updatedCompanyVehicle, userId);
        }

        public async Task<bool> DeleteCompanyVehicleAsync(Guid compVehId, Guid id)
        {
            // TODO: Dodaj updatedByUserId i soft delete
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager))
            {
                var companyVehicle = await GetCompanyVehicleByIdAsync(compVehId);
                if (companyVehicle == null)
                {
                    return false;
                }
                var managers = await _managerService.GetAllCompanyManagersAsync(companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return false;
            }
            return await _repository.DeleteCompanyVehicleAsync(compVehId, userId);
        }
    }
}