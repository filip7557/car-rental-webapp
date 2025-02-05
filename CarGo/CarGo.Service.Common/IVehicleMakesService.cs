using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface IVehicleMakesService
    {
        public Task<List<VehicleMakes>?> GetAllAsync();
        public Task<VehicleMakes?> GetByIdAsync(Guid id);
    }
}