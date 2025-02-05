using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IVehicleStatusService
    {
        public Task<List<VehicleStatus>?> GetAllAsync();

        public Task<VehicleStatus?> GetByIdAsync(Guid id);
    }
}