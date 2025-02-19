using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleModelRepository _vehicleModelRepository;
        private ITokenService _tokenService;


        public VehicleModelService(IVehicleModelRepository repository, ITokenService tokenService)
        {
            _vehicleModelRepository = repository;
            _tokenService = tokenService;
        }

        public async Task<List<VehicleModel>?> GetAllAsync()
        {
            return await _vehicleModelRepository.GetAllAsync();
        }

        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(VehicleModel vehicleModel)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _vehicleModelRepository.AddAsync(vehicleModel, userId);
        }

        public async Task DeleteAsync(Guid vehicleId)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _vehicleModelRepository.DeleteAsync(vehicleId, userId);
        }
    }
}