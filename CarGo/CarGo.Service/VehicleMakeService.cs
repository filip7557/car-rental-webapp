using CarGo.Service.Common;
using CarGo.Model;
using CarGo.Repository.Common;

namespace Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IVehicleMakeRepository _vehicleMakesRepository;

        public VehicleMakeService(IVehicleMakeRepository repository)
        {
            _vehicleMakesRepository = repository;
        }

        public async Task<List<VehicleMake>?> GetAllAsync()
        {
            return await _vehicleMakesRepository.GetAllAsync();
        }

        public async Task<VehicleMake?> GetByIdAsync(Guid id)
        {
            return await _vehicleMakesRepository.GetByIdAsync(id);
        }
    }
}