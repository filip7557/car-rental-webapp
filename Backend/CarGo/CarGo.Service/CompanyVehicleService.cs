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
        private readonly IVehicleMakeService _vehicleMake;
        private readonly ICarColorService _vehicleColorService;
        private readonly IVehicleTypeService _vehicleTypeService;


        public CompanyVehicleService(IVehicleTypeService vehicleTypeService, ICarColorService vehicleColorService, ICompanyVehicleRepository repository, ITokenService tokenService, IManagerService managerService, IVehicleModelService vehicleModelService, ICompanyService companyService, IVehicleMakeService vehicleMakeService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _managerService = managerService;
            _vehicleModelService = vehicleModelService;
            _companyService = companyService;
            _vehicleMake = vehicleMakeService;
            _vehicleColorService = vehicleColorService;
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<List<CompanyVehicleDTO>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter)
        {
            var companyVehicles = await _repository.GetAllCompanyVehiclesAsync(sorting, paging, filter);
            var companyVehicleList = new List<CompanyVehicleDTO>();
            foreach(var companyVehicle in companyVehicles){
                var vehicleModel = await _vehicleModelService.GetByIdAsync(companyVehicle.VehicleModelId);
                var vehicleMake = await _vehicleMake.GetByIdAsync(vehicleModel.MakeId);
                var vehicleType = await _vehicleTypeService.GetByIdAsync(vehicleModel.TypeId);
                var vehicleColor = await _vehicleColorService.GetByIdAsync(companyVehicle.ColorId);
                var company = await _companyService.GetCompanyAsync((Guid)companyVehicle.CompanyId);
                var companyVehicleDTO = new CompanyVehicleDTO
                {
                    CompanyVehicleId = (Guid)companyVehicle.Id,
                    VehicleMake = vehicleMake.Name,
                    VehicleModel = vehicleModel.Name!,
                    ImageUrl = companyVehicle.ImageUrl,
                    CompanyName = company!.Name,
                    CompanyId = company.Id,
                    PlateNumber = companyVehicle.PlateNumber,
                    DailyPrice = companyVehicle.DailyPrice,
                    Color = vehicleColor.Name,
                    EnginePower = vehicleModel.EnginePower,
                    VehicleType = vehicleType!.Name,
                    isActive = companyVehicle.IsActive
                };
                companyVehicleList.Add(companyVehicleDTO);
            }
            return companyVehicleList;
        }


        public async Task<CompanyVehicleDTO?> GetCompanyVehicleByIdAsync(Guid id)
        {
            var companyVehicle = await _repository.GetCompanyVehicleByIdAsync(id);
            if (companyVehicle == null)
            {
                return null;
            }

            var vehicleModel = await _vehicleModelService.GetByIdAsync(companyVehicle.VehicleModelId);
            var vehicleMake = await _vehicleMake.GetByIdAsync(vehicleModel.MakeId);
            var vehicleType = await _vehicleTypeService.GetByIdAsync(vehicleModel.TypeId);
            var vehicleColor = await _vehicleColorService.GetByIdAsync(companyVehicle.ColorId);
            var company = await _companyService.GetCompanyAsync((Guid)companyVehicle.CompanyId);

            return new CompanyVehicleDTO
            {
                CompanyVehicleId = (Guid)companyVehicle.Id,
                VehicleMake = vehicleMake.Name,
                VehicleModel = vehicleModel.Name!,
                ImageUrl = companyVehicle.ImageUrl,
                CompanyName = company!.Name,
                CompanyId = company.Id,
                PlateNumber = companyVehicle.PlateNumber,
                DailyPrice = companyVehicle.DailyPrice,
                Color = vehicleColor.Name,
                EnginePower = vehicleModel.EnginePower,
                VehicleType = vehicleType!.Name,
                isActive = companyVehicle.IsActive
            };
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