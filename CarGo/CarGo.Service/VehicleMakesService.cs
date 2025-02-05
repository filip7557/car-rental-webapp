using CarGo.Service.Common;
using CarGo.Model;
using CarGo.Repository.Common;

namespace Service
{
    public class VehicleMakesService : IVehicleMakesService
    {
        private IVehicleMakesRepository _vehicleMakesRepository;

        public VehicleMakesService(IVehicleMakesRepository repository)
        {
            _vehicleMakesRepository = repository;
        }

        public async Task<List<VehicleMakes>?> GetAllAsync()
        {
            return await _vehicleMakesRepository.GetAllAsync();
        }

        public async Task<VehicleMakes?> GetByIdAsync(Guid id)
        {
            return await _vehicleMakesRepository.GetByIdAsync(id);
        }
    }
}