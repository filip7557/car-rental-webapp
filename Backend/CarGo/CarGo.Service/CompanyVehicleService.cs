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
        private readonly ICompanyService _companyService;
        private readonly IVehicleModelService _vehicleModelService;


        public CompanyVehicleService(ICompanyVehicleRepository repository, ITokenService tokenService, IManagerService managerService, IVehicleModelService vehicleModelService, ICompanyService companyService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _managerService = managerService;
            _vehicleModelService = vehicleModelService;
            _companyService = companyService;
        }

        public async Task<List<CompanyVehicleDTO>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter)
        {
            var companyVehicles = await _repository.GetAllCompanyVehiclesAsync(sorting, paging, filter);
            var companyVehicleList = new List<CompanyVehicleDTO>();
            foreach(var companyVehicle in companyVehicles){
                var vehicleModel = await _vehicleModelService.GetByIdAsync(companyVehicle.VehicleModelId);
                var company = await _companyService.GetCompanyAsync((Guid)companyVehicle.CompanyId);
                var companyVehicleDTO = new CompanyVehicleDTO
                {
                    CompanyVehicleId = (Guid)companyVehicle.Id,
                    VehicleModel = vehicleModel!.Name!,
                    CompanyName = company!.Name,
                    PlateNumber = companyVehicle.PlateNumber,
                    DailyPrice = companyVehicle.DailyPrice,
                };
                companyVehicleList.Add(companyVehicleDTO);
            }
            return companyVehicleList;
        }


        public async Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id)
        {
            
            return await _repository.GetCompanyVehicleByIdAsync(id);
        }

        public async Task<bool> AddCompanyVehicleAsync(CompanyVehicle companyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager.ToString()))
            {
                companyVehicle.CompanyId = await _managerService.GetCompanyIdByUserIdAsync(userId);
                var managers = await _managerService.GetAllCompanyManagersAsync((Guid)companyVehicle.CompanyId);
                if (companyVehicle.CompanyId == Guid.Empty)
                    return false;
            }
            return await _repository.AddCompanyVehicleAsync(companyVehicle, userId);
        }

        public async Task<bool> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle)
        {
            var userId = _tokenService.GetCurrentUserId();
            var roleName = _tokenService.GetCurrentUserRoleName();
            if (roleName.Equals(RoleName.Manager.ToString()))
            {
                var managers = await _managerService.GetAllCompanyManagersAsync((Guid)updatedCompanyVehicle.CompanyId);
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
            if (roleName.Equals(RoleName.Manager.ToString()))
            {
                var companyVehicle = await GetCompanyVehicleByIdAsync(compVehId);
                if (companyVehicle == null)
                {
                    return false;
                }
                var managers = await _managerService.GetAllCompanyManagersAsync((Guid)companyVehicle.CompanyId);
                if (!managers.Any(p => p.Id == userId))
                    return false;
            }
            return await _repository.DeleteCompanyVehicleAsync(compVehId, userId);
        }
    }
}