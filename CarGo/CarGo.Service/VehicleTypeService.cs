using CarGo.Service.Common;
using CarGo.Model;
using CarGo.Repository.Common;

namespace Service
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleTypeService(IVehicleTypeRepository repository)
        {
            _vehicleTypeRepository = repository;
        }

        public async Task<List<VehicleType>?> GetAllAsync()
        {
            return await _vehicleTypeRepository.GetAllAsync();
        }

        public async Task<VehicleType?> GetByIdAsync(Guid id)
        {
            return await _vehicleTypeRepository.GetByIdAsync(id);
        }
    }
}