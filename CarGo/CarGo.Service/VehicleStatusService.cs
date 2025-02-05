using CarGo.Service.Common;
using CarGo.Model;
using CarGo.Repository.Common;

namespace Service
{
    public class VehicleStatusService : IVehicleStatusService
    {
        private IVehicleStatusRepository _vehicleStatusRepository;

        public VehicleStatusService(IVehicleStatusRepository repository)
        {
            _vehicleStatusRepository = repository;
        }

        public async Task<List<VehicleStatus>?> GetAllAsync()
        {
            return await _vehicleStatusRepository.GetAllAsync();
        }

        public async Task<VehicleStatus?> GetByIdAsync(Guid id)
        {
            return await _vehicleStatusRepository.GetByIdAsync(id);
        }
    }
}