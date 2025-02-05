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
        public async Task AddAsync(VehicleModel vehicleModel){}
        public async Task DeleteAsync(Guid id){}

    }
}