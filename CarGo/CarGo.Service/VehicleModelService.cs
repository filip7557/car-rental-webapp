using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleModelRepository _vehicleModelRepository;

        public VehicleModelService(IVehicleModelRepository repository)
        {
            _vehicleModelRepository = repository;
        }

        public async Task<List<VehicleModel>?> GetAllAsync()
        {
            return await _vehicleModelRepository.GetAllAsync();
        }

        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(VehicleModel vehicleModel, Guid userId){
            await _vehicleModelRepository.AddAsync(vehicleModel, userId);
        }
        public async Task DeleteAsync(Guid vehicleId, Guid id){
            await _vehicleModelRepository.DeleteAsync(vehicleId,id);
        }

    }
}